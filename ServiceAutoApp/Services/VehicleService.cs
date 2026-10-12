using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceAutoApp.Repositories.Interfaces;
using ServiceAutoApp.Models;
using ServiceAutoApp.Services.Interfaces;

namespace ServiceAutoApp.Services;

public class VehicleService(
    IVehicleRepository vehicleRepository,
    ICustomerRepository customerRepository) : IVehicleService
{
    public Task<List<Vehicle>> GetAllAsync() => vehicleRepository.GetAllWithCustomerAsync();

    public Task<Vehicle?> GetByIdAsync(int id) => vehicleRepository.GetByIdWithCustomerAsync(id);

    public async Task<List<SelectListItem>> GetCustomerOptionsAsync()
    {
        var customers = await customerRepository.GetAllAsync();
        return customers
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.FullName} ({c.Email})"
            })
            .ToList();
    }

    public Task CreateAsync(Vehicle vehicle) => vehicleRepository.AddAsync(vehicle);

    public async Task<bool> UpdateAsync(int id, Vehicle vehicle)
    {
        if (id != vehicle.Id || !await vehicleRepository.ExistsAsync(id))
        {
            return false;
        }

        await vehicleRepository.UpdateAsync(vehicle);
        return true;
    }

    public Task<Vehicle?> GetForDeleteAsync(int id) => vehicleRepository.GetByIdWithCustomerAsync(id);

    public async Task DeleteAsync(int id)
    {
        var item = await vehicleRepository.GetByIdWithCustomerAsync(id);
        if (item is null)
        {
            return;
        }

        await vehicleRepository.DeleteAsync(item);
    }
}
