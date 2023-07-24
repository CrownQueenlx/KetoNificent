using KetoNificent.Data.Entities;
using KetoNificent.Models.Product;

namespace KetoNificent.Services.Product;

public interface IProductService
{
    Task<ProductEntity?> CreateProductAsync(ProductDetailVM request);
    Task<ProductEntity?> GetAllProductsAsync();
    Task<ProductEntity?> GetProductByIdAsync(int prodId);
    Task<List<ProductEntity>> GetProductDisplayAsync(int prodId);
    // Task<bool> JoinProductServingAsync(int id);
    Task<bool> UpdateProductByIdAsync(ProductDetailVM request);
    Task<bool> DeleteProductAsync(int ProdId);





}