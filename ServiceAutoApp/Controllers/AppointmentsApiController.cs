using Microsoft.AspNetCore.Mvc;
using ServiceAutoApp.Services.Interfaces;
using ServiceAutoApp.ViewModels;

namespace ServiceAutoApp.Controllers;

[ApiController]
[Route("api/appointments")]
public class AppointmentsApiController(IAppointmentBookingService appointmentBookingService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            var appointmentId = await appointmentBookingService.BookAsync(request);
            return Ok(new
            {
                message = "Programarea a fost inregistrata cu succes.",
                appointmentId
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
