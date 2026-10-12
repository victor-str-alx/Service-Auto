using Microsoft.EntityFrameworkCore;
using ServiceAutoApp.Data;
using ServiceAutoApp.Models;
using ServiceAutoApp.Repositories.Interfaces;

namespace ServiceAutoApp.Repositories;

public class AppointmentServiceRepository(ApplicationDbContext context) : IAppointmentServiceRepository
{
    public Task<List<AppointmentService>> GetAllWithDetailsAsync() =>
        context.AppointmentServices
            .Include(x => x.Appointment)
            .ThenInclude(x => x.Customer)
            .Include(x => x.ServiceType)
            .OrderByDescending(x => x.AppointmentId)
            .ToListAsync();

    public async Task DeleteByAppointmentIdAsync(int appointmentId)
    {
        var links = await context.AppointmentServices
            .Where(x => x.AppointmentId == appointmentId)
            .ToListAsync();
        if (links.Count == 0)
        {
            return;
        }

        context.AppointmentServices.RemoveRange(links);
        await context.SaveChangesWithRetryAsync();
    }

    public async Task AddAsync(AppointmentService appointmentService)
    {
        context.AppointmentServices.Add(appointmentService);
        await context.SaveChangesWithRetryAsync();
    }
}
