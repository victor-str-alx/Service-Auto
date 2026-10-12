namespace ServiceAutoApp.Models;

public class AppointmentService
{
    public int AppointmentId { get; set; }
    public Appointment? Appointment { get; set; }

    public int ServiceTypeId { get; set; }
    public ServiceType? ServiceType { get; set; }
}
