namespace BidByBits.Data.Models;

public class Item
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal StartPrice { get; set; }
    public bool BestOffer { get; set; }
    public ItemType Type { get; set; }
    public string? ImageUrl { get; set; }
    public int OwnerId { get; set; }
    public int CategoryId { get; set; }
    public ItemStatus Status { get; set; } = ItemStatus.Active;
    public DateTime? AuctionStart { get; set; }
    public DateTime? AuctionEnd { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User Owner { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public ICollection<Bid> Bids { get; set; } = new List<Bid>();
}
