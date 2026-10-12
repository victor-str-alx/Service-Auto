using Microsoft.AspNetCore.Mvc;
using ServiceAutoApp.Services.Interfaces;

namespace ServiceAutoApp.Controllers;

public class AppointmentServicesController(IAppointmentManagementService appointmentService) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await appointmentService.GetAllAppointmentServicesAsync());
    }
}
