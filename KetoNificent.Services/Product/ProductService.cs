// using KetoNificent.Data;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Configuration;
// using System.Security.Claims;
// using Microsoft.EntityFrameworkCore;
// using KetoNificent.Models.Product;
// using KetoNificent.Data.Entities;
// using Microsoft.AspNetCore.Mvc;

// namespace KetoNificent.Services.Product;

// public class ProductService : IProductService
// {
//     private readonly int _userId;
//     private readonly AppDbContext _dbContext;
//     public ProductService(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext, IConfiguration config)
//     {
//         var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
//         var identifierClaimType = config["ClaimTypes:Id"];
//         var value = userClaims?.FindFirst(identifierClaimType)?.Value;
//         var validId = int.TryParse(value, out _userId);
//         if (!validId)
//             throw new Exception("Attempted to build ProductService without User Id claim.");
//         _dbContext = dbContext;
//     }
//     public async Task<IActionResult?> CreateProductAsync(ProductModel request)
//     {
//         var productEntity = new ProductEntity
//         {
//             Id = request.Id,
//             Name = request.Name,
//             User = _userId
//         };

//         _dbContext.ProductService
//         .Add(productEntity);
//         var numberOfChanges = await _dbContext.SaveChangesAsync();

//         if (numberOfChanges == 1)
//         {
//             ProductService response = new()
//             {
// throw new NotImplementedException()
//             }
//         }
//     }
// }