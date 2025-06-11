using BasicMicroservice.Shared.Events.Common;
using BasicMicroservice.Shared.Messages;

namespace BasicMicroservice.Shared.Events;

public record PaymentFailedEvent(Guid OrderId, string Message,List<OrderItemMessage> OrderItems) : IEvent;
