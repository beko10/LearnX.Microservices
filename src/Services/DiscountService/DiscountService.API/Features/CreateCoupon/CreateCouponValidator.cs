using FluentValidation;

namespace DiscountService.API.Features.CreateCoupon;

public class CreateCouponValidator : AbstractValidator<CreateCouponRequest>
{
    public CreateCouponValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MinimumLength(3).WithMessage("Code must be at least 3 characters.")
            .MaximumLength(20).WithMessage("Code cannot exceed 20 characters.")
            .Matches("^[A-Za-z0-9]+$").WithMessage("Code can only contain letters and numbers.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");

        RuleFor(x => x.DiscountPercentage)
            .InclusiveBetween(0, 100).WithMessage("Discount percentage must be between 0 and 100.");

        RuleFor(x => x.DiscountAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount amount cannot be negative.");

        RuleFor(x => x.UsageLimit)
            .GreaterThanOrEqualTo(0).WithMessage("Usage limit cannot be negative.");

        RuleFor(x => x.ValidFrom)
            .NotEmpty().WithMessage("Valid from date is required.");

        RuleFor(x => x.ValidUntil)
            .NotEmpty().WithMessage("Valid until date is required.")
            .GreaterThan(x => x.ValidFrom).WithMessage("Valid until must be after valid from.");
    }
}
