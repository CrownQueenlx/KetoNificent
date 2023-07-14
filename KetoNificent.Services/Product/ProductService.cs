using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using KetoNificent.Data;
using KetoNificent.Data.Entities;
using KetoNificent.Models.Product;

namespace KetoNificent.Services.Product;

public class ProductService : IProductService
{
    private readonly int _userId;
    private readonly AppDbContext _dbContext;
    public ProductService(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext, IConfiguration config)
    {
        var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
        var identifierClaimType = config["ClaimTypes:Id"];
        var value = userClaims?.FindFirst(identifierClaimType)?.Value;
        var validId = int.TryParse(value, out _userId);
        if (!validId)
            throw new Exception("Attempted to build ProductService without User Id claim.");
        _dbContext = dbContext;
    }

    // product needs a name and Id [Post]
    public async Task<ProductEntity> CreateProductAsync(ProductModel request)
    {
        var productEntity = new ProductEntity()
        {
            Name = request.Name,
            User = _userId
        };
        _dbContext.Products.Add(productEntity);
        var numberOfChanges = await _dbContext.SaveChangesAsync();
        if (numberOfChanges == 1)
        {

            ProductEntity response = new()
            {
                Id = productEntity.Id,
                Name = productEntity.Name
            };
            return response;
        }
        return null;
    }

    // product needs to be seen [Get]
    // how the product to join into serving
    public async Task<ProductEntity?> GetProductByIdAsync(int ProdId)
    {
        // Find first product with given Id and User matching _userId
        var productEntity = await _dbContext.Products
            .FirstOrDefaultAsync(y => y.Id == ProdId && y.User == _userId
            );
        // If productEntity is null -> return null
        // Otherwise initialize and return new
        return productEntity is null ? null : new ProductEntity
        {
            Id = productEntity.Id,
            Name = productEntity.Name
        };
    }

    // Update Product Name [Put]

    // Delete Product [Delete]

    


}