using System.ComponentModel.DataAnnotations;

namespace ServiceAutoApp.ViewModels;

public class UpdateProfileVehicleRequest
{
    [Required, EmailAddress, StringLength(120)]
    public string Email { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int VehicleId { get; set; }

    [Required, StringLength(50)]
    public string Brand { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Model { get; set; } = string.Empty;

    [Required, StringLength(20)]
    public string RegistrationNumber { get; set; } = string.Empty;
}
