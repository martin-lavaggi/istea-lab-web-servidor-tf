using Customers.Domain.Models.ValueObjects;

namespace Customers.Domain.Models.DTOs;

public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Address Address { get; set; } = new();
    public DateTime RegistrationDate { get; set; }
}
