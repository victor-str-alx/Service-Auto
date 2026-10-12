using System.ComponentModel.DataAnnotations;

namespace ServiceAutoApp.Models;

public class Appointment
{
    public int Id { get; set; }

    [Required]
    public DateTime AppointmentDate { get; set; }

    [Required, StringLength(30)]
    public string TimeSlot { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Notes { get; set; }

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public int VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public ICollection<AppointmentService> AppointmentServices { get; set; } = new List<AppointmentService>();
}
