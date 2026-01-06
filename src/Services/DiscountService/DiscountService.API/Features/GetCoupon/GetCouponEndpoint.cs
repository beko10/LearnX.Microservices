using MediatR;

namespace DiscountService.API.Features.GetCoupon;

public static class GetCouponEndpoint
{
    public static IEndpointRouteBuilder MapGetCouponEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v{version:apiVersion}/coupons/{id}", async (
            string id,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(new GetCouponRequest { Id = id }, cancellationToken);

            return response.Result.IsSuccess
                ? Results.Ok(response.Result.Data)
                : Results.NotFound(response.Result);
        })
        .WithName("GetCoupon")
        .WithTags("Coupons")
        .WithDescription("Get a coupon by ID")
        .Produces<CouponDto>(200)
        .Produces(404);

        return app;
    }
}
