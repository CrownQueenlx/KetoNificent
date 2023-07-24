using KetoNificent.Data.Entities;
using KetoNificent.Models.Serving;
using KetoNificent.Services.Serving;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KetoNificent.Controllers;

public class ServingController : Controller
{
    private readonly IServingService _service;
    public ServingController(IServingService service)
    {
        _service = service;
    }

    // Get Serving
    public IActionResult Index()
    {
        return View();
    }

    // Get Serving details
    [HttpGet]
    public async Task<IActionResult> Detail(int? id)
    {
        await _service.GetServingByNameAsync();
        if (id is null)
        {
            return RedirectToAction("Index");
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }

        return View();
    }
    // Get: Serving/Create
    public IActionResult Create(int servingId)
    {
        return View();
    }

    // Post: Product/Create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ServingCreateVM serving)
    {
        await _service.CreateServingAsync(serving);
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(serving);
    }

    // Get: Serving/Edit
    [HttpPut]
    public async Task<IActionResult> Edit(ServingEntity id)
    {
        await _service.UpdateServingByIdAsync(id);
        if (id is null)
        {
            return RedirectToAction("Index");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, ServingEntity serving)
    {
        await _service.UpdateServingByIdAsync(serving);
        if (id != serving.Id)
        {
            return RedirectToAction("Index");
        }
        else if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }
        else
        {

            return View(serving);
        }
    }

    // Get Serving/Delete
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteServingAsync(id);
        if (!ModelState.IsValid)
        {
            TempData["ErrorMsg"] = $"Product #{id} does not exist.";
            return RedirectToAction(nameof(Index));
        }
        {
            TempData["Msg"] = $"Product #{id} has been deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}