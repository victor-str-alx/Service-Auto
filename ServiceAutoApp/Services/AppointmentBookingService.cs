using ServiceAutoApp.Models;
using ServiceAutoApp.Repositories.Interfaces;
using ServiceAutoApp.Services.Interfaces;
using ServiceAutoApp.ViewModels;

namespace ServiceAutoApp.Services;

public class AppointmentBookingService(
    ICustomerRepository customerRepository,
    IVehicleRepository vehicleRepository,
    IServiceTypeRepository serviceTypeRepository,
    IAppointmentRepository appointmentRepository,
    IAppointmentServiceRepository appointmentServiceRepository) : IAppointmentBookingService
{
    public async Task<int> BookAsync(CreateAppointmentRequest request)
    {
        var customer = await customerRepository.GetByEmailAsync(request.Email);
        if (customer is null)
        {
            customer = new Customer
            {
                FullName = request.FullName.Trim(),
                Email = request.Email.Trim(),
                Phone = request.Phone.Trim()
            };
            await customerRepository.AddAsync(customer);
        }

        Vehicle? vehicle = null;
        if (request.VehicleId.HasValue)
        {
            vehicle = await vehicleRepository.GetByIdWithCustomerAsync(request.VehicleId.Value);
            if (vehicle is null || vehicle.CustomerId != customer.Id)
            {
                throw new InvalidOperationException("Vehicul invalid pentru clientul autentificat.");
            }
        }
        else if (!string.IsNullOrWhiteSpace(request.CarText))
        {
            var carParts = request.CarText.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            var brand = carParts.Length > 0 ? carParts[0] : "Necunoscuta";
            var model = carParts.Length > 1 ? carParts[1] : "Model";

            vehicle = await vehicleRepository.GetByCustomerAndCarAsync(customer.Id, brand, model);
            if (vehicle is null)
            {
                vehicle = new Vehicle
                {
                    Brand = brand,
                    Model = model,
                    RegistrationNumber = $"WEB-{Guid.NewGuid().ToString("N")[..8].ToUpperInvariant()}",
                    CustomerId = customer.Id
                };
                await vehicleRepository.AddAsync(vehicle);
            }
        }
        else
        {
            throw new InvalidOperationException("Clientul nu are vehicul selectat.");
        }

        var serviceType = await serviceTypeRepository.GetByNameAsync(request.ServiceName);
        if (serviceType is null)
        {
            serviceType = new ServiceType
            {
                Name = request.ServiceName.Trim(),
                Description = "Creat automat din formularul public de programari.",
                Price = 0
            };
            await serviceTypeRepository.AddAsync(serviceType);
        }

        var appointment = new Appointment
        {
            AppointmentDate = request.PreferredDate.ToDateTime(TimeOnly.MinValue),
            TimeSlot = request.TimeSlot.Trim(),
            Notes = request.Notes?.Trim(),
            CustomerId = customer.Id,
            VehicleId = vehicle.Id
        };

        await appointmentRepository.AddAsync(appointment);

        await appointmentServiceRepository.AddAsync(new AppointmentService
        {
            AppointmentId = appointment.Id,
            ServiceTypeId = serviceType.Id
        });

        return appointment.Id;
    }
}
