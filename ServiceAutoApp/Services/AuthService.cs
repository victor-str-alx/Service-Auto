using Microsoft.AspNetCore.Identity;
using ServiceAutoApp.Models;
using ServiceAutoApp.Repositories.Interfaces;
using ServiceAutoApp.Services.Interfaces;
using ServiceAutoApp.ViewModels;

namespace ServiceAutoApp.Services;

public class AuthService(
    ICustomerRepository customerRepository,
    IPasswordHasher<Customer> passwordHasher) : IAuthService
{
    public async Task<Customer?> RegisterAsync(RegisterRequest request)
    {
        var existing = await customerRepository.GetByEmailAsync(request.Email);
        if (existing is not null)
        {
            return null;
        }

        var customer = new Customer
        {
            FullName = request.FullName.Trim(),
            Email = request.Email.Trim(),
            Phone = request.Phone.Trim()
        };
        customer.PasswordHash = passwordHasher.HashPassword(customer, request.Password);

        await customerRepository.AddAsync(customer);
        return customer;
    }

    public async Task<Customer?> LoginAsync(LoginRequest request)
    {
        var customer = await customerRepository.GetByEmailAsync(request.Email);
        if (customer is null || string.IsNullOrWhiteSpace(customer.PasswordHash))
        {
            return null;
        }

        var verifyResult = passwordHasher.VerifyHashedPassword(customer, customer.PasswordHash, request.Password);
        return verifyResult == PasswordVerificationResult.Success ? customer : null;
    }
}
