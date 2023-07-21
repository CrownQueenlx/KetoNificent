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
        if (ingred  == null)
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
            return View(entity);

        }
            return RedirectToAction(nameof(Index));
    }
    // Update Ingredient

    // Delete Ingredient
}