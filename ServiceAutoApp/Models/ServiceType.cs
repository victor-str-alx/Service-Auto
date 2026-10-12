using System.ComponentModel.DataAnnotations;

namespace ServiceAutoApp.Models;

public class ServiceType
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0, 10000)]
    public decimal Price { get; set; }

    public ICollection<AppointmentService> AppointmentServices { get; set; } = new List<AppointmentService>();
}
