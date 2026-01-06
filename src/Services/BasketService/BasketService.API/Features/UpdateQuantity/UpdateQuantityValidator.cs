using FluentValidation;

namespace BasketService.API.Features.UpdateQuantity;

public class UpdateQuantityValidator : AbstractValidator<UpdateQuantityRequest>
{
    public UpdateQuantityValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("CourseId is required.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.")
            .LessThanOrEqualTo(10).WithMessage("Quantity cannot exceed 10.");
    }
}
