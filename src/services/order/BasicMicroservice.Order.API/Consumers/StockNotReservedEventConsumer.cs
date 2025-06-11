using BasicMicroservice.Order.API.Features.Orders;
using BasicMicroservice.Order.API.Repositories;
using BasicMicroservice.Shared.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BasicMicroservice.Order.API.Consumers;

public class StockNotReservedEventConsumer(OrderDbContext dbContext) : IConsumer<StockNotReservedEvent>
{
    public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
    {
        System.Diagnostics.Debugger.Break();
        
        Features.Orders.Order? order =
            await dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == context.Message.OrderId);

        if (order is null)
            return;

        order.OrderStatus = OrderStatus.Failed;
        
        await dbContext.SaveChangesAsync();
    }
}