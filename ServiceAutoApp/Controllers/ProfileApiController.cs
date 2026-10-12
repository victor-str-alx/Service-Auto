using Microsoft.AspNetCore.Mvc;
using ServiceAutoApp.Services.Interfaces;
using ServiceAutoApp.ViewModels;

namespace ServiceAutoApp.Controllers;

[ApiController]
[Route("api/profile")]
public class ProfileApiController(IProfileVehicleService profileVehicleService) : ControllerBase
{
    [HttpGet("vehicles")]
    public async Task<IActionResult> GetVehicles([FromQuery] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest(new { message = "Email-ul este obligatoriu." });
        }

        var vehicles = await profileVehicleService.GetVehiclesByEmailAsync(email);
        return Ok(vehicles.Select(v => new
        {
            id = v.Id,
            brand = v.Brand,
            model = v.Model,
            registrationNumber = v.RegistrationNumber
        }));
    }

    [HttpGet("appointments")]
    public async Task<IActionResult> GetAppointments([FromQuery] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest(new { message = "Email-ul este obligatoriu." });
        }

        var appointments = await profileVehicleService.GetAppointmentsByEmailAsync(email);
        return Ok(appointments.Select(a => new
        {
            id = a.Id,
            appointmentDate = a.AppointmentDate,
            timeSlot = a.TimeSlot,
            notes = a.Notes,
            vehicle = a.Vehicle is null ? null : new
            {
                brand = a.Vehicle.Brand,
                model = a.Vehicle.Model,
                registrationNumber = a.Vehicle.RegistrationNumber
            },
            serviceName = a.AppointmentServices.FirstOrDefault()?.ServiceType?.Name
        }));
    }

    [HttpPost("vehicles")]
    public async Task<IActionResult> AddVehicle([FromBody] CreateProfileVehicleRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var vehicle = await profileVehicleService.AddVehicleAsync(request);
        if (vehicle is null)
        {
            return NotFound(new { message = "Clientul nu exista." });
        }

        return Ok(new
        {
            id = vehicle.Id,
            brand = vehicle.Brand,
            model = vehicle.Model,
            registrationNumber = vehicle.RegistrationNumber
        });
    }

    [HttpPut("vehicles")]
    public async Task<IActionResult> UpdateVehicle([FromBody] UpdateProfileVehicleRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var vehicle = await profileVehicleService.UpdateVehicleAsync(request);
        if (vehicle is null)
        {
            return NotFound(new { message = "Vehiculul nu exista pentru acest client." });
        }

        return Ok(new
        {
            id = vehicle.Id,
            brand = vehicle.Brand,
            model = vehicle.Model,
            registrationNumber = vehicle.RegistrationNumber
        });
    }

    [HttpDelete("vehicles")]
    public async Task<IActionResult> DeleteVehicle([FromBody] DeleteProfileVehicleRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var deleted = await profileVehicleService.DeleteVehicleAsync(request);
        if (!deleted)
        {
            return NotFound(new { message = "Vehiculul nu exista pentru acest client." });
        }

        return Ok(new { message = "Vehicul sters cu succes." });
    }

    [HttpDelete("appointments")]
    public async Task<IActionResult> CancelAppointment([FromQuery] string email, [FromQuery] int appointmentId)
    {
        if (string.IsNullOrWhiteSpace(email) || appointmentId <= 0)
        {
            return BadRequest(new { message = "Email-ul si appointmentId sunt obligatorii." });
        }

        var cancelled = await profileVehicleService.CancelAppointmentAsync(email, appointmentId);
        if (!cancelled)
        {
            return NotFound(new { message = "Programarea nu exista pentru acest client." });
        }

        return Ok(new { message = "Programarea a fost anulata." });
    }
}
