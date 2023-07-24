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
    public IActionResult Index()
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
    public async Task<IActionResult> Create(ProductDetailVM product)
    {
        await _service.CreateProductAsync(product);
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // Get: Product/Edit
    public async Task<IActionResult> Edit(ProductDetailVM request)
    {
        await _service.UpdateProductByIdAsync(request);
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, ProductEditVM product)
    {
        await _service.GetProductByIdAsync(id);
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(product);
        }
    }

    // Get: Product/Delete
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _service.GetProductByIdAsync(id);
        if (entity is null)
        {
            TempData["ErrorMsg"] = $"Product #{id} does not exist";
            return RedirectToAction(nameof(Index));
        }
        {
            await _service.DeleteProductAsync(id);
            TempData["HttpResponseMessage"] = $"Product #{id} has been deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}