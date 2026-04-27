using Customers.Domain.Models.Entities;

namespace Customers.Domain.Repositories;

/// <summary>
/// Contrato del repositorio de Customers. La implementación concreta vive en la capa Infrastructure.
/// </summary>
public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<Customer> AddCustomerAsync(Customer customer);
    Task<Customer?> UpdateCustomerAsync(int id, Customer customer);
    Task<bool> DeleteCustomerAsync(int id);
}
