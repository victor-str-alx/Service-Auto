using System.ComponentModel.DataAnnotations;

namespace ServiceAutoApp.Models;

public class Employee
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required, StringLength(80)]
    public string Role { get; set; } = string.Empty;

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
