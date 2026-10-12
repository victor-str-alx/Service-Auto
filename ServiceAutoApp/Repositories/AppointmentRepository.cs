using Microsoft.EntityFrameworkCore;
using ServiceAutoApp.Data;
using ServiceAutoApp.Models;
using ServiceAutoApp.Repositories.Interfaces;

namespace ServiceAutoApp.Repositories;

public class AppointmentRepository(ApplicationDbContext context) : IAppointmentRepository
{
    public Task<List<Appointment>> GetAllWithDetailsAsync() =>
        context.Appointments
            .Include(x => x.Customer)
            .Include(x => x.Vehicle)
            .Include(x => x.Employee)
            .Include(x => x.AppointmentServices)
                .ThenInclude(x => x.ServiceType)
            .OrderByDescending(x => x.AppointmentDate)
            .ToListAsync();

    public Task<List<Appointment>> GetByCustomerIdWithDetailsAsync(int customerId) =>
        context.Appointments
            .Include(x => x.Customer)
            .Include(x => x.Vehicle)
            .Include(x => x.Employee)
            .Include(x => x.AppointmentServices)
                .ThenInclude(x => x.ServiceType)
            .Where(x => x.CustomerId == customerId)
            .OrderByDescending(x => x.AppointmentDate)
            .ToListAsync();

    public Task<Appointment?> GetByIdWithDetailsAsync(int id) =>
        context.Appointments
            .Include(x => x.Customer)
            .Include(x => x.Vehicle)
            .Include(x => x.Employee)
            .Include(x => x.AppointmentServices)
                .ThenInclude(x => x.ServiceType)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(Appointment appointment)
    {
        context.Appointments.Add(appointment);
        await context.SaveChangesWithRetryAsync();
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        context.Appointments.Update(appointment);
        await context.SaveChangesWithRetryAsync();
    }

    public async Task DeleteAsync(Appointment appointment)
    {
        context.Appointments.Remove(appointment);
        await context.SaveChangesWithRetryAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        context.Appointments.AnyAsync(x => x.Id == id);
}
