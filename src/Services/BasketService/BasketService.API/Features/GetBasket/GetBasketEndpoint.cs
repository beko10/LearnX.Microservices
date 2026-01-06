using MediatR;

namespace BasketService.API.Features.GetBasket;

public static class GetBasketEndpoint
{
    public static IEndpointRouteBuilder MapGetBasketEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v{version:apiVersion}/basket", async (
            string userId,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(new GetBasketRequest { UserId = userId }, cancellationToken);

            return response.Result.IsSuccess
                ? Results.Ok(response.Result.Data)
                : Results.NotFound(response.Result);
        })
        .WithName("GetBasket")
        .WithTags("Basket")
        .WithDescription("Get the user's basket")
        .Produces<BasketDto>(200)
        .Produces(404);

        return app;
    }
}
