using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceAutoApp.Models;
using ServiceAutoApp.Repositories.Interfaces;
using ServiceAutoApp.Services.Interfaces;

namespace ServiceAutoApp.Services;

public class AppointmentManagementService(
    IAppointmentRepository appointmentRepository,
    IAppointmentServiceRepository appointmentServiceRepository,
    ICustomerRepository customerRepository,
    IVehicleRepository vehicleRepository,
    IEmployeeRepository employeeRepository,
    IServiceTypeRepository serviceTypeRepository) : IAppointmentManagementService
{
    public Task<List<Appointment>> GetAllAsync() => appointmentRepository.GetAllWithDetailsAsync();

    public Task<Appointment?> GetByIdAsync(int id) => appointmentRepository.GetByIdWithDetailsAsync(id);

    public Task<List<AppointmentService>> GetAllAppointmentServicesAsync() => appointmentServiceRepository.GetAllWithDetailsAsync();

    public async Task<List<SelectListItem>> GetCustomerOptionsAsync()
    {
        var items = await customerRepository.GetAllAsync();
        return items.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = $"{x.FullName} ({x.Email})"
        }).ToList();
    }

    public async Task<List<SelectListItem>> GetVehicleOptionsAsync()
    {
        var items = await vehicleRepository.GetAllWithCustomerAsync();
        return items.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = $"{x.Brand} {x.Model} - {x.RegistrationNumber}"
        }).ToList();
    }

    public async Task<List<SelectListItem>> GetEmployeeOptionsAsync()
    {
        var items = await employeeRepository.GetAllAsync();
        var result = items.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = $"{x.FullName} ({x.Role})"
        }).ToList();
        result.Insert(0, new SelectListItem { Value = "", Text = "Neasignat" });
        return result;
    }

    public async Task<List<SelectListItem>> GetServiceTypeOptionsAsync()
    {
        var items = await serviceTypeRepository.GetAllAsync();
        return items.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = $"{x.Name} ({x.Price} lei)"
        }).ToList();
    }

    public async Task CreateAsync(Appointment appointment, int serviceTypeId)
    {
        await appointmentRepository.AddAsync(appointment);
        await appointmentServiceRepository.AddAsync(new AppointmentService
        {
            AppointmentId = appointment.Id,
            ServiceTypeId = serviceTypeId
        });
    }

    public async Task<bool> UpdateAsync(int id, Appointment appointment, int serviceTypeId)
    {
        if (id != appointment.Id || !await appointmentRepository.ExistsAsync(id))
        {
            return false;
        }

        await appointmentRepository.UpdateAsync(appointment);
        await appointmentServiceRepository.DeleteByAppointmentIdAsync(id);
        await appointmentServiceRepository.AddAsync(new AppointmentService
        {
            AppointmentId = id,
            ServiceTypeId = serviceTypeId
        });
        return true;
    }

    public Task<Appointment?> GetForDeleteAsync(int id) => appointmentRepository.GetByIdWithDetailsAsync(id);

    public async Task DeleteAsync(int id)
    {
        var item = await appointmentRepository.GetByIdWithDetailsAsync(id);
        if (item is null)
        {
            return;
        }

        await appointmentServiceRepository.DeleteByAppointmentIdAsync(id);
        await appointmentRepository.DeleteAsync(item);
    }
}
