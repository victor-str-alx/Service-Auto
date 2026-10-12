using ServiceAutoApp.Models;

namespace ServiceAutoApp.Repositories.Interfaces;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<Customer?> GetByEmailAsync(string email);
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Customer customer);
    Task<bool> ExistsAsync(int id);
}
