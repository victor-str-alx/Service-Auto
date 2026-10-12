using ServiceAutoApp.Models;

namespace ServiceAutoApp.Services.Interfaces;

public interface IServiceTypeService
{
    Task<List<ServiceType>> GetAllAsync();
    Task<ServiceType?> GetByIdAsync(int id);
    Task CreateAsync(ServiceType serviceType);
    Task<bool> UpdateAsync(int id, ServiceType serviceType);
    Task<ServiceType?> GetForDeleteAsync(int id);
    Task DeleteAsync(int id);
}
