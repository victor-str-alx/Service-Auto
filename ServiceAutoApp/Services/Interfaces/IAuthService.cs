using ServiceAutoApp.Models;
using ServiceAutoApp.ViewModels;

namespace ServiceAutoApp.Services.Interfaces;

public interface IAuthService
{
    Task<Customer?> RegisterAsync(RegisterRequest request);
    Task<Customer?> LoginAsync(LoginRequest request);
}
