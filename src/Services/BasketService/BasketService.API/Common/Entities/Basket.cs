namespace BasketService.API.Common.Entities;

public class Basket
{
    public string UserId { get; set; } = default!;
    public List<BasketItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
