using FluentValidation;

namespace BasketService.API.Features.GetBasket;

public class GetBasketValidator : AbstractValidator<GetBasketRequest>
{
    public GetBasketValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotNull().WithMessage("UserId cannot be null.")
            .MinimumLength(1).WithMessage("UserId cannot be empty.");
    }
}
