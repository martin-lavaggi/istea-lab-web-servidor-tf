using Microsoft.EntityFrameworkCore;
using Orders.Domain.Models.Aggregates;
using Orders.Domain.Repositories;
using Orders.Infrastructure.Data;

namespace Orders.Infrastructure.Repositories;

/// <summary>
/// Implementación de IOrderRepository sobre Entity Framework Core y SQL Server.
/// </summary>
public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.Items)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Order> AddOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }
}
