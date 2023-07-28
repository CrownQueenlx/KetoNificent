using KetoNificent.Data.Entities;
using KetoNificent.Models.Product;

namespace KetoNificent.Services.Product;

public interface IProductService
{
    Task<ProductEntity> CreateProductAsync(ProductCreateVM model);
    Task<ProductEntity?> CreateProductListAsync(ProductCreateVM model);
    Task<IEnumerable<ProductIndexVM>> GetAllProductsAsync(ProductEntity model);
    Task<ProductEntity?> GetProductByIdAsync(int model);
    Task<List<ProductEntity>> GetProductDisplayAsync(int model);
    Task<bool> UpdateProductByIdAsync(ProductDetailVM model);
    Task<bool> DeleteProductAsync(int model);





}