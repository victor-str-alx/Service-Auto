using System.ComponentModel.DataAnnotations;

namespace ServiceAutoApp.Models;

public class Vehicle
{
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string Brand { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Model { get; set; } = string.Empty;

    [Required, StringLength(20)]
    public string RegistrationNumber { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Selecteaza un client valid.")]
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
