using FitUI.Models;
using FitUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitUI.Controllers;

public class KundenController : Controller
{
    private readonly IKundeApiClient _kunden;

    public KundenController(IKundeApiClient kunden)
    {
        _kunden = kunden;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var kunden = await _kunden.GetAllAsync(ct);
        return View(kunden);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(KundeCreateViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(model);
        await _kunden.CreateAsync(model, ct);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var kunde = await _kunden.GetByIdAsync(id, ct);
        if (kunde == null) return NotFound();
        return View(kunde);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, KundeEditViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(model);
        await _kunden.UpdateAsync(id, model, ct);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _kunden.DeleteAsync(id, ct);
        return RedirectToAction(nameof(Index));
    }
}
