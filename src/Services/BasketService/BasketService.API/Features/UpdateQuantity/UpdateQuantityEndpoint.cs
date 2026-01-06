using MediatR;

namespace BasketService.API.Features.UpdateQuantity;

public static class UpdateQuantityEndpoint
{
    public static IEndpointRouteBuilder MapUpdateQuantityEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/api/v{version:apiVersion}/basket/items/{courseId}", async (
            string userId,
            string courseId,
            int quantity,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(new UpdateQuantityRequest 
            { 
                UserId = userId, 
                CourseId = courseId,
                Quantity = quantity
            }, cancellationToken);

            return response.Result.IsSuccess
                ? Results.Ok(response.Result.SuccessMessage)
                : Results.NotFound(response.Result);
        })
        .WithName("UpdateBasketItemQuantity")
        .WithTags("Basket")
        .WithDescription("Update the quantity of an item in the basket")
        .Produces(200)
        .Produces(404);

        return app;
    }
}
