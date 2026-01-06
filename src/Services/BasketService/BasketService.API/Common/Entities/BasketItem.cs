namespace BasketService.API.Common.Entities;

public class BasketItem
{
    public string CourseId { get; set; } = default!;
    public string CourseName { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 1;
}
