namespace BasicMicroservice.Shared.Messages;

public record OrderItemMessage(string ProductId, int Count);