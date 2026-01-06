using MediatR;

namespace DiscountService.API.Features.CreateCoupon;

public static class CreateCouponEndpoint
{
    public static IEndpointRouteBuilder MapCreateCouponEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v{version:apiVersion}/coupons", async (
            CreateCouponRequest request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(request, cancellationToken);

            return response.Result.IsSuccess
                ? Results.Created($"/api/v1/coupons/{response.Result.Data}", new { id = response.Result.Data })
                : Results.BadRequest(response.Result);
        })
        .WithName("CreateCoupon")
        .WithTags("Coupons")
        .WithDescription("Create a new discount coupon")
        .Produces(201)
        .Produces(400);

        return app;
    }
}
