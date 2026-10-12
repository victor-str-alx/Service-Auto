using ServiceAutoApp.Models;

namespace ServiceAutoApp.Repositories.Interfaces;

public interface IVehicleRepository
{
    Task<List<Vehicle>> GetAllWithCustomerAsync();
    Task<List<Vehicle>> GetByCustomerIdAsync(int customerId);
    Task<Vehicle?> GetByIdWithCustomerAsync(int id);
    Task<Vehicle?> GetByCustomerAndCarAsync(int customerId, string brand, string model);
    Task AddAsync(Vehicle vehicle);
    Task UpdateAsync(Vehicle vehicle);
    Task DeleteAsync(Vehicle vehicle);
    Task<bool> ExistsAsync(int id);
}
