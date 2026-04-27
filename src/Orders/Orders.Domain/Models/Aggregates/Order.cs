namespace Orders.Domain.Models.Aggregates;

public class Order
{
    private readonly List<OrderItem> _items = new();

    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public List<OrderItem> Items => _items;
    public decimal Total => _items.Sum(i => i.Subtotal);

    public void AddItem(int productId, string productName, decimal unitPrice, int quantity)
    {
        _items.Add(new OrderItem
        {
            ProductId = productId,
            ProductName = productName,
            UnitPrice = unitPrice,
            Quantity = quantity
        });
    }
}

public class OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal => UnitPrice * Quantity;
}
