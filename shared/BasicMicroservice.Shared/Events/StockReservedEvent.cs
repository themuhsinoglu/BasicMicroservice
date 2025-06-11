using BasicMicroservice.Shared.Events.Common;
using BasicMicroservice.Shared.Messages;

namespace BasicMicroservice.Shared.Events;

public record StockReservedEvent(Guid BuyerId, Guid OrderId, decimal TotalPrice,List<OrderItemMessage> OrderItems):IEvent;