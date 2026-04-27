using Microsoft.EntityFrameworkCore;
using Products.Domain.Models.Entities;
using Products.Domain.Repositories;
using Products.Infrastructure.Data;

namespace Products.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> UpdateProductAsync(int id, Product product)
    {
        var existing = await _context.Products
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (existing == null) return null;

        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Price = product.Price;
        existing.Quantity = product.Quantity;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var existing = await _context.Products
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (existing == null) return false;

        _context.Products.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
