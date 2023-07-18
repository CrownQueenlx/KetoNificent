using KetoNificent.Data;
using KetoNificent.Models;
using KetoNificent.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KetoNificent.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext _context;
    public ProductController(AppDbContext context)
    {
        _context = context;
    }

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
    public IActionResult Create(int productId)
    {
        return View();
    }
}