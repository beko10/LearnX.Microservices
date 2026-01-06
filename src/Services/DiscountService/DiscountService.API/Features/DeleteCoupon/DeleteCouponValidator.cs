using FluentValidation;

namespace DiscountService.API.Features.DeleteCoupon;

public class DeleteCouponValidator : AbstractValidator<DeleteCouponRequest>
{
    public DeleteCouponValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Coupon ID is required.")
            .NotNull().WithMessage("Coupon ID cannot be null.");
    }
}
