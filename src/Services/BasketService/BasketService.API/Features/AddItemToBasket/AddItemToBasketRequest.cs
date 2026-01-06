using BasketService.API.Features.AddItemToBasket;
using MediatR;

namespace BasketService.API.Features.AddItemToBasket;

public class AddItemToBasketRequest : IRequest<AddItemToBasketResponse>
{
    public string UserId { get; set; } = default!;
    public string CourseId { get; set; } = default!;
    public string CourseName { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 1;
}
