using BasicMicroservice.Shared.Events;
using MassTransit;

namespace BasicMicroservice.Payment.API.Consumers;

public class StockReservedEventConsumer(IPublishEndpoint publishEndpoint) : IConsumer<StockReservedEvent>
{
    public async Task Consume(ConsumeContext<StockReservedEvent> context)
    {
        System.Diagnostics.Debugger.Break();
        
        //ODEME ISLEMLERINI YAP
        Random random = new();

        bool isSuccess = random.Next(0, 2) == 0; // Rastgele başarılı veya başarısız ödeme durumu

        if (isSuccess)
        {
            PaymentCompletedEvent paymentCompletedEvent = new(context.Message.OrderId);

            await publishEndpoint.Publish(paymentCompletedEvent, pipeline =>
            {
                pipeline.SetAwaitAck(true);
                pipeline.Durable = true; // fiziksel diske yazmak için.
            }, context.CancellationToken);

            await Console.Out.WriteLineAsync("Ödeme işlemi başarılı...");
        }
        else
        {
            PaymentFailedEvent paymentFailedEvent = new(context.Message.OrderId, "Ödeme işlemi başarısız oldu.",
                context.Message.OrderItems);

            await publishEndpoint.Publish(paymentFailedEvent);

            await Console.Out.WriteLineAsync("Ödeme işlemi başarısız...");
        }
    }
}