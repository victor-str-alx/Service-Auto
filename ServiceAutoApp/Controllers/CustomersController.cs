using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ServiceAutoApp.Models;
using ServiceAutoApp.Services.Interfaces;

namespace ServiceAutoApp.Controllers;

public class CustomersController(ICustomerService customerService) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await customerService.GetAllAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var customer = await customerService.GetByIdAsync(id.Value);
        if (customer is null) return NotFound();
        return View(customer);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FullName,Email,Phone")] Customer customer)
    {
        if (!ModelState.IsValid) return View(customer);
        await customerService.CreateAsync(customer);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var customer = await customerService.GetByIdAsync(id.Value);
        if (customer is null) return NotFound();
        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Phone")] Customer customer)
    {
        if (!ModelState.IsValid) return View(customer);
        var updated = await customerService.UpdateAsync(id, customer);
        if (!updated) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var customer = await customerService.GetForDeleteAsync(id.Value);
        if (customer is null) return NotFound();
        return View(customer);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await customerService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Clientul a fost sters.";
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqliteException sqliteEx && sqliteEx.SqliteErrorCode == 5)
        {
            TempData["ErrorMessage"] = "Baza de date este ocupata momentan. Incearca din nou in cateva secunde.";
        }
        catch (DbUpdateException)
        {
            TempData["ErrorMessage"] = "Clientul nu poate fi sters deoarece are vehicule/programari asociate.";
        }

        return RedirectToAction(nameof(Index));
    }
}
