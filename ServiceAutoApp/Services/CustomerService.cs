using ServiceAutoApp.Models;
using ServiceAutoApp.Repositories.Interfaces;
using ServiceAutoApp.Services.Interfaces;

namespace ServiceAutoApp.Services;

public class CustomerService(ICustomerRepository repository) : ICustomerService
{
    public Task<List<Customer>> GetAllAsync() => repository.GetAllAsync();

    public Task<Customer?> GetByIdAsync(int id) => repository.GetByIdAsync(id);

    public Task CreateAsync(Customer customer) => repository.AddAsync(customer);

    public async Task<bool> UpdateAsync(int id, Customer customer)
    {
        if (id != customer.Id || !await repository.ExistsAsync(id))
        {
            return false;
        }

        await repository.UpdateAsync(customer);
        return true;
    }

    public Task<Customer?> GetForDeleteAsync(int id) => repository.GetByIdAsync(id);

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
