using FluentValidation;

namespace BasketService.API.Features.ClearBasket;

public class ClearBasketValidator : AbstractValidator<ClearBasketRequest>
{
    public ClearBasketValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotNull().WithMessage("UserId cannot be null.");
    }
}
