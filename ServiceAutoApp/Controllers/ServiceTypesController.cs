using Microsoft.AspNetCore.Mvc;
using ServiceAutoApp.Models;
using ServiceAutoApp.Services.Interfaces;

namespace ServiceAutoApp.Controllers;

public class ServiceTypesController(IServiceTypeService serviceTypeService) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await serviceTypeService.GetAllAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var service = await serviceTypeService.GetByIdAsync(id.Value);
        if (service is null) return NotFound();
        return View(service);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,Price")] ServiceType serviceType)
    {
        if (!ModelState.IsValid) return View(serviceType);
        await serviceTypeService.CreateAsync(serviceType);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var serviceType = await serviceTypeService.GetByIdAsync(id.Value);
        if (serviceType is null) return NotFound();
        return View(serviceType);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price")] ServiceType serviceType)
    {
        if (!ModelState.IsValid) return View(serviceType);
        var updated = await serviceTypeService.UpdateAsync(id, serviceType);
        if (!updated) return NotFound();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var serviceType = await serviceTypeService.GetForDeleteAsync(id.Value);
        if (serviceType is null) return NotFound();
        return View(serviceType);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await serviceTypeService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
