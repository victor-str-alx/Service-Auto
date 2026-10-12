using ServiceAutoApp.ViewModels;

namespace ServiceAutoApp.Services.Interfaces;

public interface IAppointmentBookingService
{
    Task<int> BookAsync(CreateAppointmentRequest request);
}
