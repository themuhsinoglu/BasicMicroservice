using BasicMicroservice.Shared.Events;
using BasicMicroservice.Stock.API.Respositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BasicMicroservice.Stock.API.Consumers;

public class PaymentFailedEventConsumer(StockDbContext dbContext): IConsumer<PaymentFailedEvent>
{
    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        System.Diagnostics.Debugger.Break();
        
        foreach (var orderItem in context.Message.OrderItems)
        {
            var stock = await dbContext.Stocks.FirstAsync(s => s.ProductId == orderItem.ProductId, cancellationToken: context.CancellationToken);
            
            stock.Count += orderItem.Count;
            
            await dbContext.SaveChangesAsync(cancellationToken: context.CancellationToken);
        }
    }
}