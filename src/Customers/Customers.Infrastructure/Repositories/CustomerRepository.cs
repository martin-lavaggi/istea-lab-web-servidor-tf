using Customers.Domain.Models.Entities;
using Customers.Domain.Repositories;
using Customers.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure.Repositories;

/// <summary>
/// Implementación de ICustomerRepository sobre Entity Framework Core y SQL Server.
/// </summary>
public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return await _context.Customers
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer?> UpdateCustomerAsync(int id, Customer customer)
    {
        var existing = await _context.Customers
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (existing == null) return null;

        existing.Name = customer.Name;
        existing.Email = customer.Email;
        existing.Address = customer.Address;
        // RegistrationDate no se modifica: representa la fecha de creación del registro.

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var existing = await _context.Customers
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (existing == null) return false;

        _context.Customers.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
