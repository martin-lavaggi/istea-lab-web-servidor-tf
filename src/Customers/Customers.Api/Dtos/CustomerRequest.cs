using Customers.Domain.Models.ValueObjects;

namespace Customers.Api.Dtos;

public record CustomerRequest(string Name, string Email, Address Address);
