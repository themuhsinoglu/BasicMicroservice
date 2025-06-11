using BasicMicroservice.Order.API.Repositories;
using BasicMicroservice.Shared.Events;
using BasicMicroservice.Shared.Messages;
using MassTransit;
using MediatR;

namespace BasicMicroservice.Order.API.Features.Orders.Create;

public class CreateOrderCommandHandler(OrderDbContext context, IPublishEndpoint publishEndpoint) : IRequestHandler<CreateOrderCommand>
{
    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        System.Diagnostics.Debugger.Break();
        
        Order newOrder = new()
        {
            OrderId = Guid.NewGuid(),
            BuyerId = request.BuyerId,
            TotalPrice = request.OrderItems.Sum(item => (item.Price * item.Count)),
            OrderStatus = OrderStatus.Suspend,
            CreatedDate = DateTime.UtcNow
        };

        newOrder.OrderItems = request.OrderItems.Select(oi => new OrderItem
        {
            Count = oi.Count,
            Price = oi.Price,
            ProductId = oi.ProductId,
        }).ToList();
        
        await context.Orders.AddAsync(newOrder, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        OrderCreatedEvent orderCreatedEvent = new(newOrder.OrderId, newOrder.BuyerId, newOrder.TotalPrice,
            newOrder.OrderItems.Select(oi => new OrderItemMessage(oi.ProductId, oi.Count)).ToList());
        
        await publishEndpoint.Publish(orderCreatedEvent,pipeline=>
        {
            pipeline.SetAwaitAck(true); // ack bilgiisni bekler.
            pipeline.Durable = true; // fiziksel diske yazmak icin.
        },cancellationToken);
    }
}