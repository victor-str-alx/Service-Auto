using Microsoft.AspNetCore.Mvc;
using ServiceAutoApp.Models;
using ServiceAutoApp.Services.Interfaces;

namespace ServiceAutoApp.Controllers;

public class EmployeesController(IEmployeeService employeeService) : Controller
{
    public async Task<IActionResult> Index() => View(await employeeService.GetAllAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var employee = await employeeService.GetByIdAsync(id.Value);
        if (employee is null) return NotFound();
        return View(employee);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FullName,Role")] Employee employee)
    {
        if (!ModelState.IsValid) return View(employee);
        await employeeService.CreateAsync(employee);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var employee = await employeeService.GetByIdAsync(id.Value);
        if (employee is null) return NotFound();
        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Role")] Employee employee)
    {
        if (!ModelState.IsValid) return View(employee);
        var updated = await employeeService.UpdateAsync(id, employee);
        if (!updated) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var employee = await employeeService.GetForDeleteAsync(id.Value);
        if (employee is null) return NotFound();
        return View(employee);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await employeeService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
