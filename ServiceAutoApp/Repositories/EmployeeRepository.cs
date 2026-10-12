using Microsoft.EntityFrameworkCore;
using ServiceAutoApp.Data;
using ServiceAutoApp.Models;
using ServiceAutoApp.Repositories.Interfaces;

namespace ServiceAutoApp.Repositories;

public class EmployeeRepository(ApplicationDbContext context) : IEmployeeRepository
{
    public Task<List<Employee>> GetAllAsync() =>
        context.Employees.OrderBy(x => x.FullName).ToListAsync();

    public Task<Employee?> GetByIdAsync(int id) =>
        context.Employees.FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(Employee employee)
    {
        context.Employees.Add(employee);
        await context.SaveChangesWithRetryAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
        context.Employees.Update(employee);
        await context.SaveChangesWithRetryAsync();
    }

    public async Task DeleteAsync(Employee employee)
    {
        context.Employees.Remove(employee);
        await context.SaveChangesWithRetryAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        context.Employees.AnyAsync(x => x.Id == id);
}
