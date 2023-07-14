using KetoNificent.Data.Entities;
using KetoNificent.Models.Product;

namespace KetoNificent.Services.Product;

public interface IProductService
{
    Task<ProductEntity> CreateProductAsync(ProductModel request);
    Task<ProductEntity?> GetProductByIdAsync (int ProdId);
    // Task<bool> JoinProductServingAsync(int id);



    

    
}