using MediatR;

namespace DiscountService.API.Features.UpdateCoupon;

public static class UpdateCouponEndpoint
{
    public static IEndpointRouteBuilder MapUpdateCouponEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/api/v{version:apiVersion}/coupons/{id}", async (
            string id,
            UpdateCouponRequest request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            request.Id = id;
            var response = await mediator.Send(request, cancellationToken);

            return response.Result.IsSuccess
                ? Results.Ok(response.Result.SuccessMessage)
                : Results.BadRequest(response.Result);
        })
        .WithName("UpdateCoupon")
        .WithTags("Coupons")
        .WithDescription("Update an existing coupon")
        .Produces(200)
        .Produces(400)
        .Produces(404);

        return app;
    }
}
