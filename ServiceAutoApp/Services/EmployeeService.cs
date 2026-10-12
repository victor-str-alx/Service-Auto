using ServiceAutoApp.Models;
using ServiceAutoApp.Repositories.Interfaces;
using ServiceAutoApp.Services.Interfaces;

namespace ServiceAutoApp.Services;

public class EmployeeService(IEmployeeRepository repository) : IEmployeeService
{
    public Task<List<Employee>> GetAllAsync() => repository.GetAllAsync();

    public Task<Employee?> GetByIdAsync(int id) => repository.GetByIdAsync(id);

    public Task CreateAsync(Employee employee) => repository.AddAsync(employee);

    public async Task<bool> UpdateAsync(int id, Employee employee)
    {
        if (id != employee.Id || !await repository.ExistsAsync(id))
        {
            return false;
        }

        await repository.UpdateAsync(employee);
        return true;
    }

    public Task<Employee?> GetForDeleteAsync(int id) => repository.GetByIdAsync(id);

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
