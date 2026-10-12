using ServiceAutoApp.Models;

namespace ServiceAutoApp.Repositories.Interfaces;

public interface IAppointmentServiceRepository
{
    Task<List<AppointmentService>> GetAllWithDetailsAsync();
    Task DeleteByAppointmentIdAsync(int appointmentId);
    Task AddAsync(AppointmentService appointmentService);
}
