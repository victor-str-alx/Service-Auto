using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceAutoApp.Models;

namespace ServiceAutoApp.Services.Interfaces;

public interface IAppointmentManagementService
{
    Task<List<Appointment>> GetAllAsync();
    Task<Appointment?> GetByIdAsync(int id);
    Task<List<AppointmentService>> GetAllAppointmentServicesAsync();
    Task<List<SelectListItem>> GetCustomerOptionsAsync();
    Task<List<SelectListItem>> GetVehicleOptionsAsync();
    Task<List<SelectListItem>> GetEmployeeOptionsAsync();
    Task<List<SelectListItem>> GetServiceTypeOptionsAsync();
    Task CreateAsync(Appointment appointment, int serviceTypeId);
    Task<bool> UpdateAsync(int id, Appointment appointment, int serviceTypeId);
    Task<Appointment?> GetForDeleteAsync(int id);
    Task DeleteAsync(int id);
}
