using Microsoft.EntityFrameworkCore;
using ServiceAutoApp.Data;
using ServiceAutoApp.Models;
using ServiceAutoApp.Repositories.Interfaces;

namespace ServiceAutoApp.Repositories;

public class VehicleRepository(ApplicationDbContext context) : IVehicleRepository
{
    public Task<List<Vehicle>> GetAllWithCustomerAsync() =>
        context.Vehicles
            .Include(x => x.Customer)
            .OrderBy(x => x.Brand)
            .ThenBy(x => x.Model)
            .ToListAsync();

    public Task<List<Vehicle>> GetByCustomerIdAsync(int customerId) =>
        context.Vehicles
            .Include(x => x.Customer)
            .Where(x => x.CustomerId == customerId)
            .OrderBy(x => x.Brand)
            .ThenBy(x => x.Model)
            .ToListAsync();

    public Task<Vehicle?> GetByIdWithCustomerAsync(int id) =>
        context.Vehicles
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id);

    public Task<Vehicle?> GetByCustomerAndCarAsync(int customerId, string brand, string model) =>
        context.Vehicles.FirstOrDefaultAsync(x =>
            x.CustomerId == customerId &&
            x.Brand.ToLower() == brand.ToLower() &&
            x.Model.ToLower() == model.ToLower());

    public async Task AddAsync(Vehicle vehicle)
    {
        context.Vehicles.Add(vehicle);
        await context.SaveChangesWithRetryAsync();
    }

    public async Task UpdateAsync(Vehicle vehicle)
    {
        context.Vehicles.Update(vehicle);
        await context.SaveChangesWithRetryAsync();
    }

    public async Task DeleteAsync(Vehicle vehicle)
    {
        context.Vehicles.Remove(vehicle);
        await context.SaveChangesWithRetryAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        context.Vehicles.AnyAsync(x => x.Id == id);
}
