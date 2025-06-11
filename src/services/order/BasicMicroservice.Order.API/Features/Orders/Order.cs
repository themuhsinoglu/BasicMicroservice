namespace BasicMicroservice.Order.API.Features.Orders;

public class Order
{
    public Guid OrderId { get; set; }
    public Guid BuyerId { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime CreatedDate { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string ProductId { get; set; } =default!;
    public int Count { get; set; }
    public decimal Price { get; set; }

    public Order Order { get; set; } = default!;
}

public enum OrderStatus
{
    Suspend,
    Completed,
    Failed
}