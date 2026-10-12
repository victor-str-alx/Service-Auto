using ServiceAutoApp.Models;

namespace ServiceAutoApp.Repositories.Interfaces;

public interface IServiceTypeRepository
{
    Task<List<ServiceType>> GetAllAsync();
    Task<ServiceType?> GetByIdAsync(int id);
    Task<ServiceType?> GetByNameAsync(string name);
    Task AddAsync(ServiceType serviceType);
    Task UpdateAsync(ServiceType serviceType);
    Task DeleteAsync(ServiceType serviceType);
    Task<bool> ExistsAsync(int id);
}
