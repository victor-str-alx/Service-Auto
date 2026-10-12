using System.ComponentModel.DataAnnotations;

namespace ServiceAutoApp.ViewModels;

public class CreateAppointmentRequest
{
    [Required, StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required, StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(120)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public DateOnly PreferredDate { get; set; }

    [Required, StringLength(30)]
    public string TimeSlot { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string ServiceName { get; set; } = string.Empty;

    public int? VehicleId { get; set; }

    [StringLength(120)]
    public string? CarText { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }
}
