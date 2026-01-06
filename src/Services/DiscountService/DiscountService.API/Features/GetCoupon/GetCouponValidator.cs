using FluentValidation;

namespace DiscountService.API.Features.GetCoupon;

public class GetCouponValidator : AbstractValidator<GetCouponRequest>
{
    public GetCouponValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Coupon ID is required.")
            .NotNull().WithMessage("Coupon ID cannot be null.");
    }
}
