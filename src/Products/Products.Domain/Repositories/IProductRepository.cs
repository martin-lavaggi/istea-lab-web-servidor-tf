using Products.Domain.Models.Entities;

namespace Products.Domain.Repositories;

/// <summary>
/// Contrato del repositorio de Products. La implementación concreta vive en la capa Infrastructure.
/// </summary>
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> AddProductAsync(Product product);
    Task<Product?> UpdateProductAsync(int id, Product product);
    Task<bool> DeleteProductAsync(int id);
}
