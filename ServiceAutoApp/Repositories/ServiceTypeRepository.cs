using Microsoft.EntityFrameworkCore;
using ServiceAutoApp.Data;
using ServiceAutoApp.Models;
using ServiceAutoApp.Repositories.Interfaces;

namespace ServiceAutoApp.Repositories;

public class ServiceTypeRepository(ApplicationDbContext context) : IServiceTypeRepository
{
    public Task<List<ServiceType>> GetAllAsync() =>
        context.ServiceTypes.OrderBy(x => x.Name).ToListAsync();

    public Task<ServiceType?> GetByIdAsync(int id) =>
        context.ServiceTypes.FirstOrDefaultAsync(x => x.Id == id);

    public Task<ServiceType?> GetByNameAsync(string name) =>
        context.ServiceTypes.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

    public async Task AddAsync(ServiceType serviceType)
    {
        context.ServiceTypes.Add(serviceType);
        await context.SaveChangesWithRetryAsync();
    }

    public async Task UpdateAsync(ServiceType serviceType)
    {
        context.ServiceTypes.Update(serviceType);
        await context.SaveChangesWithRetryAsync();
    }

    public async Task DeleteAsync(ServiceType serviceType)
    {
        context.ServiceTypes.Remove(serviceType);
        await context.SaveChangesWithRetryAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        context.ServiceTypes.AnyAsync(x => x.Id == id);
}
