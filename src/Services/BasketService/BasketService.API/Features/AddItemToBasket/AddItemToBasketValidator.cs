using FluentValidation;

namespace BasketService.API.Features.AddItemToBasket;

public class AddItemToBasketValidator : AbstractValidator<AddItemToBasketRequest>
{
    public AddItemToBasketValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotNull().WithMessage("UserId cannot be null.");

        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("CourseId is required.")
            .NotNull().WithMessage("CourseId cannot be null.");

        RuleFor(x => x.CourseName)
            .NotEmpty().WithMessage("CourseName is required.")
            .MaximumLength(200).WithMessage("CourseName cannot exceed 200 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be at least 1.")
            .LessThanOrEqualTo(10).WithMessage("Quantity cannot exceed 10.");
    }
}
