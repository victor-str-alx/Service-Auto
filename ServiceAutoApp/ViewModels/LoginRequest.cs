using System.ComponentModel.DataAnnotations;

namespace ServiceAutoApp.ViewModels;

public class LoginRequest
{
    [Required, EmailAddress, StringLength(120)]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(4)]
    public string Password { get; set; } = string.Empty;
}
