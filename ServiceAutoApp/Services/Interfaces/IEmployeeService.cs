using ServiceAutoApp.Models;

namespace ServiceAutoApp.Services.Interfaces;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task CreateAsync(Employee employee);
    Task<bool> UpdateAsync(int id, Employee employee);
    Task<Employee?> GetForDeleteAsync(int id);
    Task DeleteAsync(int id);
}
