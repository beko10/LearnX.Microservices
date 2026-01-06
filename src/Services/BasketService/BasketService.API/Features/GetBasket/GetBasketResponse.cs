using BasketService.API.Common.Entities;
using BuildingBlocks.Core.Results;

namespace BasketService.API.Features.GetBasket;

public class GetBasketResponse
{
    public ServiceResult<BasketDto> Result { get; set; } = default!;
}

public class BasketDto
{
    public string UserId { get; set; } = default!;
    public List<BasketItemDto> Items { get; set; } = [];
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class BasketItemDto
{
    public string CourseId { get; set; } = default!;
    public string CourseName { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal => Price * Quantity;
}
