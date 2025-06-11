using BasicMicroservice.Shared;
using BasicMicroservice.Shared.Events;
using BasicMicroservice.Stock.API.Respositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BasicMicroservice.Stock.API.Consumers;

public class OrderCreatedEventConsumer(StockDbContext dbContext,ISendEndpointProvider sendEndpointProvider,IPublishEndpoint publishEndpoint): IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        System.Diagnostics.Debugger.Break();
        
        List<bool> stockResult = new();

        foreach (var orderItem in context.Message.OrderItems)
        {
            stockResult.Add(await dbContext.Stocks.Where(s => s.ProductId == orderItem.ProductId && s.Count >= orderItem.Count).AnyAsync(cancellationToken:context.CancellationToken));
        }

        if (stockResult.TrueForAll(sr => sr.Equals(true)))
        {
            foreach (var orderItem in context.Message.OrderItems)
            {
                var stock = await dbContext.Stocks.FirstAsync(s => s.ProductId == orderItem.ProductId, cancellationToken: context.CancellationToken);
                
                stock.Count -= orderItem.Count;
                
                await dbContext.SaveChangesAsync(cancellationToken: context.CancellationToken);
            }
            
            StockReservedEvent stockReservedEvent = new(context.Message.BuyerId,context.Message.OrderId, context.Message.TotalPrice, context.Message.OrderItems);
            
            // ISendEndpoint:
            // Bu, mesajı belirli bir kuyruğa veya hedefe göndermek için kullanılır.
            // Gönderen, mesajın kime gideceğini (hedef kuyruğu) bilir ve doğrudan o kuyruğa gönderir.
            ISendEndpoint sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqSettings.Payment_StockReservedEventQueue}"));
            await sendEndpoint.Send(stockReservedEvent, pipeline =>
            {
                pipeline.SetAwaitAck(true); // ack bilgiisni bekler
                pipeline.Durable = true; // fiziksel diske yazmak icin.
            },context.CancellationToken);
            
            await Console.Out.WriteLineAsync("Stok işlemleri başarılı...");
            
        }
        else
        {
            StockNotReservedEvent stockNotReservedEvent = new(context.Message.BuyerId, context.Message.OrderId, "...");
            
            // IPublishEndpoint:
            // Bu, bir 'olayı' (Event) yayınlamak için kullanılır.
            // Yayınlayan (publisher), mesajın kime gideceğini bilmez veya umursamaz.
            // Mesaj bir değişim noktasına (exchange/topic) gönderilir ve bu olayı dinleyen
            // tüm ilgili kuyruklar/tüketiciler (consumers) bu mesajı alır. 
            
            // MassTransit'in iç mekanizması sayesinde bu olayı dinleyen tüm tüketicilere iletilir.
            // Yani, bu olayı dinleyen 'birden fazla' servis varsa, hepsi bu mesajı alacaktır.
            // Bu, dağıtık sistemlerde olay tabanlı iletişimin ve gevşek bağlılığın temelidir.
            
            await publishEndpoint.Publish(stockNotReservedEvent, pipeline =>
            {
                pipeline.SetAwaitAck(true); // ack bilgiisni bekler.
                pipeline.Durable = true; // fiziksel diske yazmak icin.
            },context.CancellationToken);
            
            await Console.Out.WriteLineAsync("Stok işlemleri başarısız...");
        }
    }
}