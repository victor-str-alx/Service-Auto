using ServiceAutoApp.Models;

namespace ServiceAutoApp.Services.Interfaces;

public interface ICustomerService
{
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task CreateAsync(Customer customer);
    Task<bool> UpdateAsync(int id, Customer customer);
    Task<Customer?> GetForDeleteAsync(int id);
    Task DeleteAsync(int id);
}
