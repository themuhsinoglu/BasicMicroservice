using BasicMicroservice.Order.API.Features;
using BasicMicroservice.Order.API.Features.Orders;
using Microsoft.EntityFrameworkCore;

namespace BasicMicroservice.Order.API.Repositories;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<Features.Orders.Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
}