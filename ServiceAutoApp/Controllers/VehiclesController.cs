using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ServiceAutoApp.Models;
using ServiceAutoApp.Services.Interfaces;

namespace ServiceAutoApp.Controllers;

public class VehiclesController(IVehicleService vehicleService) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await vehicleService.GetAllAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var vehicle = await vehicleService.GetByIdAsync(id.Value);
        if (vehicle is null) return NotFound();
        return View(vehicle);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.CustomerOptions = await vehicleService.GetCustomerOptionsAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Brand,Model,RegistrationNumber,CustomerId")] Vehicle vehicle)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.CustomerOptions = await vehicleService.GetCustomerOptionsAsync();
            return View(vehicle);
        }

        await vehicleService.CreateAsync(vehicle);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var vehicle = await vehicleService.GetByIdAsync(id.Value);
        if (vehicle is null) return NotFound();

        ViewBag.CustomerOptions = await vehicleService.GetCustomerOptionsAsync();
        return View(vehicle);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model,RegistrationNumber,CustomerId")] Vehicle vehicle)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.CustomerOptions = await vehicleService.GetCustomerOptionsAsync();
            return View(vehicle);
        }

        var updated = await vehicleService.UpdateAsync(id, vehicle);
        if (!updated) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var vehicle = await vehicleService.GetForDeleteAsync(id.Value);
        if (vehicle is null) return NotFound();
        return View(vehicle);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await vehicleService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Vehiculul a fost sters.";
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqliteException sqliteEx && sqliteEx.SqliteErrorCode == 5)
        {
            TempData["ErrorMessage"] = "Baza de date este ocupata momentan. Incearca din nou in cateva secunde.";
        }
        catch (DbUpdateException)
        {
            TempData["ErrorMessage"] = "Vehiculul nu poate fi sters deoarece este folosit in programari.";
        }

        return RedirectToAction(nameof(Index));
    }
}
