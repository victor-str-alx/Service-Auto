using ServiceAutoApp.Models;
using ServiceAutoApp.Repositories.Interfaces;
using ServiceAutoApp.Services.Interfaces;
using ServiceAutoApp.ViewModels;

namespace ServiceAutoApp.Services;

public class ProfileVehicleService(
    ICustomerRepository customerRepository,
    IVehicleRepository vehicleRepository,
    IAppointmentRepository appointmentRepository,
    IAppointmentServiceRepository appointmentServiceRepository) : IProfileVehicleService
{
    public async Task<List<Vehicle>> GetVehiclesByEmailAsync(string email)
    {
        var customer = await customerRepository.GetByEmailAsync(email);
        if (customer is null)
        {
            return [];
        }

        return await vehicleRepository.GetByCustomerIdAsync(customer.Id);
    }

    public async Task<List<Appointment>> GetAppointmentsByEmailAsync(string email)
    {
        var customer = await customerRepository.GetByEmailAsync(email);
        if (customer is null)
        {
            return [];
        }

        return await appointmentRepository.GetByCustomerIdWithDetailsAsync(customer.Id);
    }

    public async Task<bool> CancelAppointmentAsync(string email, int appointmentId)
    {
        var customer = await customerRepository.GetByEmailAsync(email);
        if (customer is null)
        {
            return false;
        }

        var appointment = await appointmentRepository.GetByIdWithDetailsAsync(appointmentId);
        if (appointment is null || appointment.CustomerId != customer.Id)
        {
            return false;
        }

        await appointmentServiceRepository.DeleteByAppointmentIdAsync(appointmentId);
        await appointmentRepository.DeleteAsync(appointment);
        return true;
    }

    public async Task<Vehicle?> AddVehicleAsync(CreateProfileVehicleRequest request)
    {
        var customer = await customerRepository.GetByEmailAsync(request.Email);
        if (customer is null)
        {
            return null;
        }

        var existing = await vehicleRepository.GetByCustomerAndCarAsync(
            customer.Id,
            request.Brand.Trim(),
            request.Model.Trim());
        if (existing is not null)
        {
            return existing;
        }

        var vehicle = new Vehicle
        {
            Brand = request.Brand.Trim(),
            Model = request.Model.Trim(),
            RegistrationNumber = request.RegistrationNumber.Trim(),
            CustomerId = customer.Id
        };

        await vehicleRepository.AddAsync(vehicle);
        return vehicle;
    }

    public async Task<Vehicle?> UpdateVehicleAsync(UpdateProfileVehicleRequest request)
    {
        var customer = await customerRepository.GetByEmailAsync(request.Email);
        if (customer is null)
        {
            return null;
        }

        var vehicle = await vehicleRepository.GetByIdWithCustomerAsync(request.VehicleId);
        if (vehicle is null || vehicle.CustomerId != customer.Id)
        {
            return null;
        }

        vehicle.Brand = request.Brand.Trim();
        vehicle.Model = request.Model.Trim();
        vehicle.RegistrationNumber = request.RegistrationNumber.Trim();
        await vehicleRepository.UpdateAsync(vehicle);
        return vehicle;
    }

    public async Task<bool> DeleteVehicleAsync(DeleteProfileVehicleRequest request)
    {
        var customer = await customerRepository.GetByEmailAsync(request.Email);
        if (customer is null)
        {
            return false;
        }

        var vehicle = await vehicleRepository.GetByIdWithCustomerAsync(request.VehicleId);
        if (vehicle is null || vehicle.CustomerId != customer.Id)
        {
            return false;
        }

        await vehicleRepository.DeleteAsync(vehicle);
        return true;
    }
}
