using BasicMicroservice.Shared.Events.Common;
using BasicMicroservice.Shared.Messages;

namespace BasicMicroservice.Shared.Events;

public record OrderCreatedEvent(Guid OrderId, Guid BuyerId, decimal TotalPrice,List<OrderItemMessage> OrderItems) : IEvent;