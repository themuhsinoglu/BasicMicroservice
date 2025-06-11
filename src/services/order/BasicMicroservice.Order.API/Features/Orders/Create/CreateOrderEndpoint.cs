using MediatR;

namespace BasicMicroservice.Order.API.Features.Orders.Create;

public static class CreateOrderEndpoint
{
    public static RouteGroupBuilder CreateOrderGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (CreateOrderCommand command, IMediator mediator) => await mediator.Send(command));

        return group;
    }   
}