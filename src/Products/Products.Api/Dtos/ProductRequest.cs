namespace Products.Api.Dtos;

public record ProductRequest(string Name, string Description, decimal Price, int Quantity);
