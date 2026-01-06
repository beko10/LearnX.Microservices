using MediatR;

namespace BasketService.API.Features.RemoveItemFromBasket;

public static class RemoveItemFromBasketEndpoint
{
    public static IEndpointRouteBuilder MapRemoveItemFromBasketEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/v{version:apiVersion}/basket/items/{courseId}", async (
            string userId,
            string courseId,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(new RemoveItemFromBasketRequest 
            { 
                UserId = userId, 
                CourseId = courseId 
            }, cancellationToken);

            return response.Result.IsSuccess
                ? Results.Ok(response.Result.SuccessMessage)
                : Results.NotFound(response.Result);
        })
        .WithName("RemoveItemFromBasket")
        .WithTags("Basket")
        .WithDescription("Remove an item from the user's basket")
        .Produces(200)
        .Produces(404);

        return app;
    }
}
