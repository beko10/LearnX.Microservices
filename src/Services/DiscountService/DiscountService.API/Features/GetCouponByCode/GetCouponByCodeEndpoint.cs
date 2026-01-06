using DiscountService.API.Features.GetCoupon;
using MediatR;

namespace DiscountService.API.Features.GetCouponByCode;

public static class GetCouponByCodeEndpoint
{
    public static IEndpointRouteBuilder MapGetCouponByCodeEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v{version:apiVersion}/coupons/code/{code}", async (
            string code,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(new GetCouponByCodeRequest { Code = code }, cancellationToken);

            return response.Result.IsSuccess
                ? Results.Ok(response.Result.Data)
                : Results.NotFound(response.Result);
        })
        .WithName("GetCouponByCode")
        .WithTags("Coupons")
        .WithDescription("Get a coupon by its code")
        .Produces<CouponDto>(200)
        .Produces(404);

        return app;
    }
}
