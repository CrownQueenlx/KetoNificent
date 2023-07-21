using KetoNificent.Data;
using KetoNificent.Data.Entities;
using KetoNificent.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KetoNificent.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext _context;
    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    // Get product
    public async Task<IActionResult> Index()
    {
        List<ProductIndexVM> products = await _context.Products
        .Select(y => new ProductIndexVM
        {
            Id = y.Id,
            Name = y.Name,
            User = y.User
        })
        .ToListAsync();

        return View(products);
    }
    // Get product details
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }
        var product = await _context.Products.FindAsync(id);
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
    // Get: Product/Create
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
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // Get: Product/Edit
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var product = await _context.Products.FindAsync(id);
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