using KetoNificent.Data.Entities;
using KetoNificent.Models.Product;

namespace KetoNificent.Services.Product;

public interface IProductService
{
    Task<ProductEntity> CreateProductAsync(ProductCreateVM model);
    Task<ProductEntity?> CreateProductListAsync(ProductCreateVM model);
    Task<IEnumerable<ProductEntity?>> GetAllProductsAsync();

    Task<ProductEntity?> GetProductByIdAsync(int prodId);
    Task<List<ProductEntity>> GetProductDisplayAsync(int prodId);
    // Task<bool> JoinProductServingAsync(int id);
    Task<bool> UpdateProductByIdAsync(ProductDetailVM request);
    Task<bool> DeleteProductAsync(int ProdId);





}