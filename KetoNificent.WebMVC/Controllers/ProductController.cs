using KetoNificent.Data;
using KetoNificent.Data.Entities;
using KetoNificent.Models.Product;
using KetoNificent.Services.Ingredient;
using KetoNificent.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KetoNificent.Controllers;

[Authorize]
public class ProductController : Controller
{
    private readonly IProductService _service;
    public ProductController(IProductService service)
    {
        _service = service;
    }

    // Get: Product/List of Ingredients
    public async Task<IActionResult> Display(int id)
    {
        var name = await _service.GetProductDisplayAsync(id);
        var vm = new ProductIndexVM
        {
            NameList = new()
        };
        return View(vm);
    }
    // Get product
    public Task<ProductIndexVM> Index()
    {
        return View();
    }
    // Get product details
    public async Task<IActionResult> Details(int id)
    {
        var product = await _service.GetProductByIdAsync(id);
        if (product is null)
        {
            return NotFound();
        }
        var vm = new ProductDetailVM
        {
            Id = product.Id,
            Name = product.Name,
        };
        return View(vm);
    }

    // Post: Product/Create
    public IActionResult Create(int productId)
    {
        return View();
    }

    // Post: Product/Create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Id,User")] ProductCreateVM product)
    {
        if (ModelState.IsValid)
        {
            var entity = new ProductEntity
            {
                Name = product.Name,
                User = product.User
            };
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            return View(product);
        }
        return RedirectToAction(nameof(Index));
    }

    // Get: Product/Edit
    public async Task<IActionResult> Edit(int? id)
    {
        var product = await _context.Products.FindAsync(id);
        if (id == null)
        {
            return NotFound();
        }
        if (product == null)
        {
            return NotFound();
        }

        var vm = new ProductEditVM
        {
            Id = product.Id,
            Name = product.Name,
            User = product.User
        };
        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,User")] ProductEditVM product)
    {
        if (id != product.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            var entity = await _context.Products.FindAsync(id);
            if (entity is null)
                return RedirectToAction(nameof(Index));
            entity.Name = product.Name;
            entity.Id = product.Id;
            entity.User = product.User;

            _context.Products.Update(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // Get: Product/Delete
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Products.FindAsync(id);
        if (entity is null)
        {
            TempData["ErrorMsg"] = $"Product #{id} does not exist";
            return RedirectToAction(nameof(Index));
        }
        var numOfChanges = await _context.SaveChangesAsync();
        if (numOfChanges == 1)
        {
            _context.Products.Remove(entity);
            TempData["HttpResponseMessage"] = $"Product #{id} has been deleted";
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}