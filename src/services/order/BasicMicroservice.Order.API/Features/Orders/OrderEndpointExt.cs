using BasicMicroservice.Order.API.Features.Orders.Create;

namespace BasicMicroservice.Order.API.Features.Orders;

public static class OrderEndpointExt
{
    public static void AddOrderGroupEndpointExt(this WebApplication app)
    {
        app.MapGroup("api/orders")
            .WithTags("Orders")
            .CreateOrderGroupItemEndpoint();
    }
}