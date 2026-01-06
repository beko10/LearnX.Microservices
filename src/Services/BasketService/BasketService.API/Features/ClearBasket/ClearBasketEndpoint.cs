using MediatR;

namespace BasketService.API.Features.ClearBasket;

public static class ClearBasketEndpoint
{
    public static IEndpointRouteBuilder MapClearBasketEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/v{version:apiVersion}/basket", async (
            string userId,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(new ClearBasketRequest { UserId = userId }, cancellationToken);

            return response.Result.IsSuccess
                ? Results.Ok(response.Result.SuccessMessage)
                : Results.NotFound(response.Result);
        })
        .WithName("ClearBasket")
        .WithTags("Basket")
        .WithDescription("Clear all items from the user's basket")
        .Produces(200)
        .Produces(404);

        return app;
    }
}
