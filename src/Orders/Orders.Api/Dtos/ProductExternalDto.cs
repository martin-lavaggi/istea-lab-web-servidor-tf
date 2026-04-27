namespace Orders.Api.Dtos;

public record ProductExternalDto(int Id, string Name, string Description, decimal Price, int Quantity);
