using FluentValidation;

namespace BasketService.API.Features.RemoveItemFromBasket;

public class RemoveItemFromBasketValidator : AbstractValidator<RemoveItemFromBasketRequest>
{
    public RemoveItemFromBasketValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotNull().WithMessage("UserId cannot be null.");

        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("CourseId is required.")
            .NotNull().WithMessage("CourseId cannot be null.");
    }
}
