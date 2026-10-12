using ServiceAutoApp.Models;
using ServiceAutoApp.ViewModels;

namespace ServiceAutoApp.Services.Interfaces;

public interface IProfileVehicleService
{
    Task<List<Vehicle>> GetVehiclesByEmailAsync(string email);
    Task<List<Appointment>> GetAppointmentsByEmailAsync(string email);
    Task<bool> CancelAppointmentAsync(string email, int appointmentId);
    Task<Vehicle?> AddVehicleAsync(CreateProfileVehicleRequest request);
    Task<Vehicle?> UpdateVehicleAsync(UpdateProfileVehicleRequest request);
    Task<bool> DeleteVehicleAsync(DeleteProfileVehicleRequest request);
}
