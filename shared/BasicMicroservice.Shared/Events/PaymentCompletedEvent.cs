using BasicMicroservice.Shared.Events.Common;

namespace BasicMicroservice.Shared.Events;

public record PaymentCompletedEvent(Guid OrderId) : IEvent;