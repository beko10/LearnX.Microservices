using FluentValidation;

namespace DiscountService.API.Features.GetCouponByCode;

public class GetCouponByCodeValidator : AbstractValidator<GetCouponByCodeRequest>
{
    public GetCouponByCodeValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Coupon code is required.")
            .NotNull().WithMessage("Coupon code cannot be null.");
    }
}
