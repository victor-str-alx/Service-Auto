using Microsoft.EntityFrameworkCore;
using ServiceAutoApp.Data;
using ServiceAutoApp.Models;
using ServiceAutoApp.Repositories.Interfaces;

namespace ServiceAutoApp.Repositories;

public class CustomerRepository(ApplicationDbContext context) : ICustomerRepository
{
    public Task<List<Customer>> GetAllAsync() =>
        context.Customers.OrderBy(x => x.FullName).ToListAsync();

    public Task<Customer?> GetByIdAsync(int id) =>
        context.Customers.FirstOrDefaultAsync(x => x.Id == id);

    public Task<Customer?> GetByEmailAsync(string email) =>
        context.Customers.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

    public async Task AddAsync(Customer customer)
    {
        context.Customers.Add(customer);
        await context.SaveChangesWithRetryAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        context.Customers.Update(customer);
        await context.SaveChangesWithRetryAsync();
    }

    public async Task DeleteAsync(Customer customer)
    {
        context.Customers.Remove(customer);
        await context.SaveChangesWithRetryAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        context.Customers.AnyAsync(x => x.Id == id);
}
