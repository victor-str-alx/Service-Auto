using System.ComponentModel.DataAnnotations;

namespace ServiceAutoApp.ViewModels;

public class DeleteProfileVehicleRequest
{
    [Required, EmailAddress, StringLength(120)]
    public string Email { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int VehicleId { get; set; }
}
