using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceAutoApp.Models;

namespace ServiceAutoApp.Services.Interfaces;

public interface IVehicleService
{
    Task<List<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetByIdAsync(int id);
    Task<List<SelectListItem>> GetCustomerOptionsAsync();
    Task CreateAsync(Vehicle vehicle);
    Task<bool> UpdateAsync(int id, Vehicle vehicle);
    Task<Vehicle?> GetForDeleteAsync(int id);
    Task DeleteAsync(int id);
}
