using Microsoft.AspNetCore.Mvc;
using ServiceAutoApp.Models;
using ServiceAutoApp.Services.Interfaces;

namespace ServiceAutoApp.Controllers;

public class AppointmentsController(IAppointmentManagementService appointmentService) : Controller
{
    public async Task<IActionResult> Index() => View(await appointmentService.GetAllAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var item = await appointmentService.GetByIdAsync(id.Value);
        if (item is null) return NotFound();
        return View(item);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateOptionsAsync();
        return View(new Appointment { AppointmentDate = DateTime.Now.AddDays(1) });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("AppointmentDate,TimeSlot,Notes,CustomerId,VehicleId,EmployeeId")] Appointment appointment, int serviceTypeId)
    {
        if (!ModelState.IsValid)
        {
            await PopulateOptionsAsync();
            return View(appointment);
        }

        await appointmentService.CreateAsync(appointment, serviceTypeId);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var item = await appointmentService.GetByIdAsync(id.Value);
        if (item is null) return NotFound();

        await PopulateOptionsAsync(item.AppointmentServices.FirstOrDefault()?.ServiceTypeId);
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,AppointmentDate,TimeSlot,Notes,CustomerId,VehicleId,EmployeeId")] Appointment appointment, int serviceTypeId)
    {
        if (!ModelState.IsValid)
        {
            await PopulateOptionsAsync(serviceTypeId);
            return View(appointment);
        }

        var updated = await appointmentService.UpdateAsync(id, appointment, serviceTypeId);
        if (!updated) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var item = await appointmentService.GetForDeleteAsync(id.Value);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await appointmentService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateOptionsAsync(int? selectedServiceTypeId = null)
    {
        ViewBag.CustomerOptions = await appointmentService.GetCustomerOptionsAsync();
        ViewBag.VehicleOptions = await appointmentService.GetVehicleOptionsAsync();
        ViewBag.EmployeeOptions = await appointmentService.GetEmployeeOptionsAsync();
        var serviceTypeOptions = await appointmentService.GetServiceTypeOptionsAsync();
        foreach (var option in serviceTypeOptions)
        {
            option.Selected = selectedServiceTypeId.HasValue && option.Value == selectedServiceTypeId.Value.ToString();
        }
        ViewBag.ServiceTypeOptions = serviceTypeOptions;
    }
}
