using KetoNificent.Data;
using KetoNificent.Data.Entities;
using KetoNificent.Models.Ingredient;
using KetoNificent.Services.Ingredient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KetoNificent.Controllers;

public class IngredientController : Controller
{
    private readonly IIngredientService _service;
    public IngredientController(IIngredientService service)
    {
        _service = service;
    }

    // Get Ingredient
    public async Task<IActionResult> Index()
    {
        // create a new VM list to display 
        List<IngredientIndexVM> ingredModel = await _service.GetIngredientsAsync();

        // show the list
        return View(ingredModel);
    }
    // Get Ingredient Details
    public async Task<IActionResult> Detail(IngredientDetailVM model)
    {
        var ingred = await _service.GetIngredientDetailsAsync(model);
    
       
        return View(ingred);
    }
    // Get: Ingredient/Create
    public IActionResult Create(int ingredId)
    {
        return View();
    }
    // Post: Ingredient/Create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(IngredientCreateVM ingredient)
    {
        if (!ModelState.IsValid)
        {
            return View(ingredient);
        }
        await _service.CreateIngredientAsync(ingredient);
        return RedirectToAction(nameof(Index));
    }
    // Get: Ingredient/Edit
    public async Task<IActionResult> Update(int id)
    {
        var model = await _service.GetIngredientByIdAsync(id);
        if (model is null)
        {
            return NotFound();
        }
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(IngredientEditVM ingredient)
    {
        var model = await _service.UpdateIngredientByIdAsync(ingredient);
        if (model is false)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    //Get: Ingredient/Delete
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _service.DeleteIngredientByIdAsync(id);
        if (entity is false)
        {
            TempData["ErrorMsg"] = $"Ingredient #{id} does not exist";
            return RedirectToAction(nameof(Index));
        }
        //DeleteIngredientByIdAsync //.Remove(entity);
        TempData["HttpResponseMessage"] = $"Product #{id} has been deleted";
        return RedirectToAction(nameof(Index));
    }
}
