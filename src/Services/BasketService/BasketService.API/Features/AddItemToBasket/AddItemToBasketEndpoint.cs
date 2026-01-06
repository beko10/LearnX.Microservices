using MediatR;

namespace BasketService.API.Features.AddItemToBasket;

public static class AddItemToBasketEndpoint
{
    public static IEndpointRouteBuilder MapAddItemToBasketEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v{version:apiVersion}/basket/items", async (
            AddItemToBasketRequest request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(request, cancellationToken);

            return response.Result.IsSuccess
                ? Results.Ok(response.Result.SuccessMessage)
                : Results.BadRequest(response.Result);
        })
        .WithName("AddItemToBasket")
        .WithTags("Basket")
        .WithDescription("Add an item to the user's basket")
        .Produces(200)
        .Produces(400);

        return app;
    }
}
