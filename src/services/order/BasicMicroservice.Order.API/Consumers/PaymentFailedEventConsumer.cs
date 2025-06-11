using BasicMicroservice.Order.API.Repositories;
using BasicMicroservice.Shared.Events;
using MassTransit;

namespace BasicMicroservice.Order.API.Consumers;

public class PaymentFailedEventConsumer(OrderDbContext dbContext) : IConsumer<PaymentFailedEvent>
{
    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        System.Diagnostics.Debugger.Break();
        
        Features.Orders.Order? order =await dbContext.Orders.FindAsync(context.Message.OrderId);
        
        if (order is null)
            return;
        
        order.OrderStatus = Features.Orders.OrderStatus.Failed;
        
        await dbContext.SaveChangesAsync();
    }
}