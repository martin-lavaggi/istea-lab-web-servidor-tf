namespace Web.Client.Models;

public record OrderRequest(int CustomerId, List<OrderItemRequest> Items);

public record OrderItemRequest(int ProductId, int Quantity);
