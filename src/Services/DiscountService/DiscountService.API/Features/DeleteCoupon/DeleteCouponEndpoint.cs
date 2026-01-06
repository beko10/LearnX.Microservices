using MediatR;

namespace DiscountService.API.Features.DeleteCoupon;

public static class DeleteCouponEndpoint
{
    public static IEndpointRouteBuilder MapDeleteCouponEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/v{version:apiVersion}/coupons/{id}", async (
            string id,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(new DeleteCouponRequest { Id = id }, cancellationToken);

            return response.Result.IsSuccess
                ? Results.Ok(response.Result.SuccessMessage)
                : Results.NotFound(response.Result);
        })
        .WithName("DeleteCoupon")
        .WithTags("Coupons")
        .WithDescription("Delete a coupon by ID")
        .Produces(200)
        .Produces(404);

        return app;
    }
}
