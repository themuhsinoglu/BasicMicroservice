using MediatR;

namespace BasicMicroservice.Order.API.Features.Orders.Create;

public record CreateOrderCommand(Guid BuyerId, List<CreateOrderItemCommand> OrderItems) : IRequest;


public record CreateOrderItemCommand(string ProductId, int Count, decimal Price);