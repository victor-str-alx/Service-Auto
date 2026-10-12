using ServiceAutoApp.Models;
using ServiceAutoApp.Repositories.Interfaces;
using ServiceAutoApp.Services.Interfaces;

namespace ServiceAutoApp.Services;

public class ServiceTypeService(IServiceTypeRepository repository) : IServiceTypeService
{
    public Task<List<ServiceType>> GetAllAsync() => repository.GetAllAsync();

    public Task<ServiceType?> GetByIdAsync(int id) => repository.GetByIdAsync(id);

    public Task CreateAsync(ServiceType serviceType) => repository.AddAsync(serviceType);

    public async Task<bool> UpdateAsync(int id, ServiceType serviceType)
    {
        if (id != serviceType.Id || !await repository.ExistsAsync(id))
        {
            return false;
        }

        await repository.UpdateAsync(serviceType);
        return true;
    }

    public Task<ServiceType?> GetForDeleteAsync(int id) => repository.GetByIdAsync(id);

    public async Task DeleteAsync(int id)
    {
        var item = await repository.GetByIdAsync(id);
        if (item is null)
        {
            return;
        }

        await repository.DeleteAsync(item);
    }
}
