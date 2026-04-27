using Orders.Domain.Models.Aggregates;

namespace Orders.Domain.Repositories;

/// <summary>
/// Contrato del repositorio de Orders. La implementación concreta vive en la capa Infrastructure.
/// </summary>
public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetOrderByIdAsync(int id);
    Task<Order> AddOrderAsync(Order order);
}
