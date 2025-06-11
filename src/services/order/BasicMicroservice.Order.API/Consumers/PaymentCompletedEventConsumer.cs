using BasicMicroservice.Order.API.Repositories;
using BasicMicroservice.Shared.Events;
using MassTransit;

namespace BasicMicroservice.Order.API.Consumers;

public class PaymentCompletedEventConsumer(OrderDbContext dbContext) : IConsumer<PaymentCompletedEvent>
{
    public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
    {
        System.Diagnostics.Debugger.Break();
        
        Features.Orders.Order? order = await dbContext.Orders.FindAsync(context.Message.OrderId);
        
        if (order is null)
            return;
        
        order.OrderStatus = Features.Orders.OrderStatus.Completed;
        
        await dbContext.SaveChangesAsync();
    }
}