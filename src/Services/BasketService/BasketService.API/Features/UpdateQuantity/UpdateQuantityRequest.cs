using MediatR;

namespace BasketService.API.Features.UpdateQuantity;

public class UpdateQuantityRequest : IRequest<UpdateQuantityResponse>
{
    public string UserId { get; set; } = default!;
    public string CourseId { get; set; } = default!;
    public int Quantity { get; set; }
}
