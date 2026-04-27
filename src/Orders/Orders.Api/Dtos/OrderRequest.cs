namespace Orders.Api.Dtos;

public record OrderRequest(int CustomerId, List<OrderItemRequest> Items);

public record OrderItemRequest(int ProductId, int Quantity);
