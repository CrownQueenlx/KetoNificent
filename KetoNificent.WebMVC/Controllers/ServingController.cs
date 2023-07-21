using KetoNificent.Data.Entities;
using KetoNificent.Models.Serving;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KetoNificent.Controllers;

public class ServingController : Controller
{
    private readonly AppDbContext _context;
    public ServingController(AppDbContext context)
    {
        _context = context;
    }
    // Get Serving
    public async Task<IActionResult> Index()
    {
        List<ServingIndexVM> servings = await _context.Servings
        .Select(y => new ServingIndexVM
        {
            Id = y.Id,
            Measurement = y.Measurement,
            Amount = y.Amount
        })
        .ToListAsync();

        return View(servings);
    }
    // Get Serving details
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }
        var serving = await _context.Servings.FindAsync(id);
        if (serving is null)
        {
            return NotFound();
        }
        var vm = new ServingDetailVM
        {
            Id = serving.Id,
            Measurement = serving.Measurement,
            Amount = serving.Amount,
            IngredientId = serving.IngredientId,            
            ProductId = serving.ProductId,
        };
        return View(vm);
    }
    // Get: Serving/Create
    public IActionResult Create(int servingId)
    {
        return View();
    }
    // Post: Product/Create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Id,User")] ServingCreateVM serving)
    {
        if (ModelState.IsValid)
        {
            var entity = new ServingEntity
            {
                Id = serving.Id,
                Measurement = serving.Measurement,
                Amount = serving.Amount,
                IngredientId = serving.IngredientId,
                ProductId = serving.ProductId
            };
            _context.Servings.Add(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(serving);
    }
    // Get: Serving/Edit
    public async Task<IActionResult> Edit(int? id)
    {
        var serving = await _context.Servings.FindAsync(id);
        if (id is null)
        {
            return NotFound();
        }
        if (serving is null)
        {
            return NotFound();
        }

        var vm = new ServingEditVM
        {
            Id = serving.Id,
            Measurement = serving.Measurement,
            Amount = serving.Amount,
            IngredientId = serving.IngredientId,
            ProductId = serving.ProductId
        };
        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,User")] ServingEditVM serving)
    {
        if (id != serving.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            var entity = await _context.Servings.FindAsync(id);
            if (entity is null)
            return RedirectToAction(nameof(Index));
            entity.Id = serving.Id;
            entity.Measurement = serving.Measurement;
            entity.Amount = serving.Amount;
            entity.IngredientId = serving.IngredientId;
            entity.ProductId = serving.ProductId;

            _context.Servings.Update(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        return View(serving);
    }

    // Get Serving/Delete
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Servings.FindAsync(id);
        if (entity is null)
        {
            TempData["ErrorMsg"] = $"Product #{id} does not exist.";
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}