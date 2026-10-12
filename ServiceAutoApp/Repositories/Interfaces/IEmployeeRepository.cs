using ServiceAutoApp.Models;

namespace ServiceAutoApp.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(Employee employee);
    Task<bool> ExistsAsync(int id);
}
