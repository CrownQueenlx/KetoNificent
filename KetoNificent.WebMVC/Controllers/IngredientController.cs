using KetoNificent.Data;
using KetoNificent.Data.Entities;
using KetoNificent.Models.Ingredient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KetoNificent.Controllers;

public class IngredientController : Controller
{
    private readonly AppDbContext _context;
    public IngredientController(AppDbContext context)
    {
        _context = context;
    }

    // Get Ingredient
    public async Task<IActionResult> Index()
    {
        // create a new VM list to display 
        List<IngredientIndexVM> ingredModel = await _context.Ingredients
        .Select(y => new IngredientIndexVM
        {
            Name = y.Name,
            NCarb = y.NCarb,
            NCarbCt = y.NCarbCt,
            DefaultMeasurement = y.DefaultMeasurement,
            DefaultAmount = y.DefaultAmount
        })
        .ToListAsync();

        // show the list we just made
        return View(ingredModel);
    }
    // Get Ingredient Details
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }
        var ingred = await _context.Ingredients.FindAsync(id);
        if (ingred == null)
        {
            return NotFound();
        }
        var vm = new IngredientDetailVM
        {
            Id = ingred.Id,
            Name = ingred.Name,
            NCarb = ingred.NCarb,
            NCarbCt = ingred.NCarbCt,
            FatCt = ingred.Fat,
            ProteinCt = ingred.Protein,
            DefaultMeasurement = ingred.DefaultMeasurement,
            DefaultAmount = ingred.DefaultAmount
        };
        return View(vm);
    }
    // Get: Ingredient/Create
    public IActionResult Create(int ingredId)
    {
        return View();
    }
    // Post: Ingredient/Create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Id,User")] Data.Entities.IngredientEntity ingredient)
    {
        if (ModelState.IsValid)
        {
            var entity = new Data.Entities.IngredientEntity
            {
                Name = ingredient.Name,
                NCarbCt = ingredient.NCarbCt,
                NCarb = ingredient.NCarb,
                Fat = ingredient.Fat,
                Protein = ingredient.Protein,
                DefaultMeasurement = ingredient.DefaultMeasurement,
                DefaultAmount = ingredient.DefaultAmount,
            };
            _context.Ingredients.Add(entity);
            await _context.SaveChangesAsync();
            return View(entity);

        }
        return RedirectToAction(nameof(Index));
    }
    // Get: Ingredient/Edit
    public async Task<IActionResult> Update(int? id)
    {
        var model = await _context.Ingredients.FindAsync(id);
        if (id is null)
        {
            return NotFound();
        }
        if (model is null)
        {
            return NotFound();
        }
        var vm = new IngredientEditVM
        {
            Id = model.Id,
            Name = model.Name,
            NCarb = model.NCarb,
            NCarbCt = model.NCarbCt,
            Fat = model.Fat,
            Protein = model.Protein,
            DefaultMeasurement = model.DefaultMeasurement,
            DefaultAmount = model.DefaultAmount

        };
        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,User")] IngredientEditVM ingredient)
    {
        if (id != ingredient.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            var entity = await _context.Ingredients.FindAsync(id);
            if (entity is null)
                return RedirectToAction(nameof(Index));
            entity.Id = ingredient.Id;
            entity.Name = ingredient.Name;
            entity.NCarb = ingredient.NCarb;
            entity.NCarbCt = ingredient.NCarbCt;
            entity.Fat = ingredient.Fat;
            entity.Protein = ingredient.Protein;
            entity.DefaultMeasurement = ingredient.DefaultMeasurement;
            entity.DefaultAmount = ingredient.DefaultAmount;

            _context.Ingredients.Update(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        return View(ingredient);
    }

    //Get: Ingredient/Delete
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Ingredients.FindAsync(id);
        if (entity is null)
        {
            TempData["ErrorMsg"] = $"Product #{id} does not exist";
            return RedirectToAction(nameof(Index));
        }
        var numOfChanges = await _context.SaveChangesAsync();
        if (numOfChanges == 1)
        {
            _context.Ingredients.Remove(entity);
            TempData["HttpResponseMessage"] = $"Product #{id} has been deleted";
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}