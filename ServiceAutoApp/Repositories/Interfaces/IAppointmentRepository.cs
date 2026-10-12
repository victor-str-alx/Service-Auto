using ServiceAutoApp.Models;

namespace ServiceAutoApp.Repositories.Interfaces;

public interface IAppointmentRepository
{
    Task<List<Appointment>> GetAllWithDetailsAsync();
    Task<List<Appointment>> GetByCustomerIdWithDetailsAsync(int customerId);
    Task<Appointment?> GetByIdWithDetailsAsync(int id);
    Task AddAsync(Appointment appointment);
    Task UpdateAsync(Appointment appointment);
    Task DeleteAsync(Appointment appointment);
    Task<bool> ExistsAsync(int id);
}
