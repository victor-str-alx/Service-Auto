using System.ComponentModel.DataAnnotations;

namespace ServiceAutoApp.Models;

public class Customer
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(120)]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    [StringLength(500)]
    public string? PasswordHash { get; set; }

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
