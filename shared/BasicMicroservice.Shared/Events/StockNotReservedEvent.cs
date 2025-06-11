using BasicMicroservice.Shared.Events.Common;

namespace BasicMicroservice.Shared.Events;

public record StockNotReservedEvent(Guid BuyerId, Guid OrderId, string Message):IEvent;