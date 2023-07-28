using Microsoft.EntityFrameworkCore;
using KetoNificent.Data.Entities;
using KetoNificent.Models.Product;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace KetoNificent.Services.Product;

public class ProductService : IProductService
{
    private readonly int _userId;
    private readonly AppDbContext _dbContext;
    public ProductService(AppDbContext dbContext, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
    {
        var user = signInManager.Context.User;
        var userIdClaim = userManager.GetUserId(user);
        int.TryParse(userIdClaim, out _userId);
        _dbContext = dbContext;
    }

    // product needs a name and Id [Post]
    public async Task<ProductEntity> CreateProductAsync(ProductCreateVM model)
    {
        var product = new ProductEntity()
        {
            Id = model.Id,
            Name = model.Name,
            UserId = _userId
        };

        _dbContext.Products.Add(product);
        var numberOfChanges = await _dbContext.SaveChangesAsync();

        if (numberOfChanges == 1)
        {
            ProductEntity response = new()
            {
                Id = product.Id,
                Name = product.Name,
                UserId = product.UserId
            };
            return response;
        }
        return null;
    }

    public async Task<ProductEntity?> CreateProductListAsync(ProductCreateVM model)
    {
        var validationResults = new List<ValidationResult>();
        var valdationContext = new ValidationContext(model, serviceProvider: null, items: null);

        if (Validator.TryValidateObject(model, valdationContext, validationResults, true))
        {
            ProductEntity response = new()
            {
                Id = model.Id,
                Name = model.Name,
                UserId = _userId,
            };
            _dbContext.Products.Add(response);
            return response;
        }
        var numberOfChanges = await _dbContext.SaveChangesAsync();
        if (numberOfChanges != 1)
        {
            return null;
        }
        return null;
    }

    // product needs to be seen [Get]
    public async Task<IEnumerable<ProductIndexVM>> GetAllProductsAsync(ProductEntity model)
    {
        var allProd = await _dbContext.Products
        .Where(prod => prod.UserId == _userId)
        .Select(prod => new ProductIndexVM
        {
            Id = prod.Id,
            Name = prod.Name,
            UserId = prod.UserId
        })
        .ToListAsync();
        return (allProd);
    }


    public async Task<ProductEntity?> GetProductByIdAsync(int model)
    {
        // Find first product with given Id and User matching _userId
        var productEntity = await _dbContext.Products
            .FirstOrDefaultAsync(y => y.Id == model && y.UserId == _userId
            );
        // If productEntity is null -> return null
        // Otherwise initialize and return new
        return productEntity is null ? null : new ProductEntity
        {
            Id = productEntity.Id,
            Name = productEntity.Name
        };
    }

    //ProductService Get to-list
    public async Task<List<ProductEntity>> GetProductDisplayAsync(int model)
    {
        // If model is null -> return null
        var pIdToSearch = model <= 0 ? null : (int?)model;

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
    public async Task<bool> UpdateProductByIdAsync(ProductDetailVM model)
    {
        // using the null conditional operator checks if null and also the User against the _userId
        var productEntity = await _dbContext.Products.FindAsync(model.Id);
        if (productEntity?.UserId != _userId)
            return false;

        // Now we update the entity's properties
        productEntity.Name = model.Name;
        // Save the changes to the database check how many rows updated
        var numberOfChanges = await _dbContext.SaveChangesAsync();
        // Save changes to the Database stated to be equal to one because should be only one row updated
        return numberOfChanges == 1;
    }

    // Delete Product [Delete]
    public async Task<bool> DeleteProductAsync(int model)
    {
        // find by Id
        var productEntity = await _dbContext.Products.FindAsync(model);

        // Validate existence and ownership
        if (productEntity?.UserId != _userId)
            return false;

        // remove Product form the DbContext and assert that the one change was saved
        _dbContext.Products.Remove(productEntity);
        return await _dbContext.SaveChangesAsync() == 1;
    }
}