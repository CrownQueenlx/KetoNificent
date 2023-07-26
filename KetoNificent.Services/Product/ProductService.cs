using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using KetoNificent.Data;
using KetoNificent.Data.Entities;
using KetoNificent.Models.Product;
using System.ComponentModel.DataAnnotations;

namespace KetoNificent.Services.Product;

public class ProductService : IProductService
{
    private readonly int _userId;
    private readonly AppDbContext _dbContext;
    public ProductService(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext, IConfiguration config)
    {
        // var userClaims = httpContextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
        // var identifierClaimType = config["ClaimTypes:Id"]!;
        // var value = userClaims!.FindFirst(identifierClaimType!)!.Value!;
        // var validId = int.TryParse(value, out _userId);
        // if (!validId)
        //     throw new Exception("Attempted to build ProductService without User Id claim.");
        _dbContext = dbContext;
    }

    // product needs a name and Id [Post]
    public async Task<ProductEntity?> CreateProductAsync(ProductDetailVM request)
    {
        var validationResults = new List<ValidationResult>();
        var valdationContext = new ValidationContext(request, serviceProvider: null, items: null);

        if (Validator.TryValidateObject(request, valdationContext, validationResults, true))
        {
            var productEntity = new ProductEntity()
            {
                Name = request.Name,
                UserId = _userId,
                Servings = (ICollection<ServingEntity>)request.Servings
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
        }
        return null;
    }

    // product needs to be seen [Get]
    public async Task<ProductEntity?> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }
    public async Task<ProductEntity?> GetProductByIdAsync(int prodId)
    {
        // Find first product with given Id and User matching _userId
        var productEntity = await _dbContext.Products
            .FirstOrDefaultAsync(y => y.Id == prodId && y.UserId == _userId
            );
        // If productEntity is null -> return null
        // Otherwise initialize and return new
        return productEntity is null ? null : new ProductEntity
        {
            Id = productEntity.Id,
            Name = productEntity.Name
        };
    }
    //ProductService Get tolist
    public async Task<List<ProductEntity>> GetProductDisplayAsync(int prodId)
    {
        // If prodId is null -> return null
        var pIdToSearch = prodId <= 0 ? null : (int?)prodId;

        // Find product with given Id and User matching _userId
        var productEntity = await _dbContext.Products
            .Where(y => y.Id == pIdToSearch && y.UserId == _userId)
            .ToListAsync();
        // Otherwise initialize and return new
        return productEntity.Select(entity => new ProductEntity
        {
            Id = entity.Id,
            Name = entity.Name
        }).ToList();
    }

    // Update Product Name [Put]
    public async Task<bool> UpdateProductByIdAsync(ProductDetailVM request)
    {
        // using the null conditional operator checks if null and also the User against the _userId
        var productEntity = await _dbContext.Products.FindAsync(request.Id);
        if (productEntity?.UserId != _userId)
            return false;

        // Now we update the entity's properties
        productEntity.Name = request.Name;
        // Save the changes to the database check how many rows updated
        var numberOfChanges = await _dbContext.SaveChangesAsync();
        // Save changes to the Database stated to be equal to one because should be only one row updated
        return numberOfChanges == 1;
    }

    // Delete Product [Delete]
    public async Task<bool> DeleteProductAsync(int ProdId)
    {
        // find by Id
        var productEntity = await _dbContext.Products.FindAsync(ProdId);

        // Validate existence and ownership
        if (productEntity?.UserId != _userId)
            return false;

        // remove Product form the DbContext and assert that the one change was saved
        _dbContext.Products.Remove(productEntity);
        return await _dbContext.SaveChangesAsync() == 1;
    }

}