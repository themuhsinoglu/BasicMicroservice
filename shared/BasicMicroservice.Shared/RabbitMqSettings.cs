namespace BasicMicroservice.Shared;

public static class RabbitMqSettings
{
    public const string Stock_OrderCreatedEventQueue = "stock-order-craeted-event-queue";
    public const string Stock_PaymentFailedEventQueue = "stock-payment-failed-event-queue";
    public const string Payment_StockReservedEventQueue = "payment-stock-reserved-event-queue";
    public const string Order_PaymentCompletedEventQueue = "order-payment-completed-event-queue";
    public const string Order_StockNotReservedEventQueue = "order-stock-not-reserved-event-queue";
    public const string Order_PaymentFailedEventQueue = "order-payment-failed-event-queue";
}