namespace BidByBits.Data.Models;

public class Bid
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PlacedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Item Item { get; set; } = null!;
    public User User { get; set; } = null!;
}
