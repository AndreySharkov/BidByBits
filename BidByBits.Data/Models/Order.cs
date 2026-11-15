namespace BidByBits.Data.Models;

public class Order
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int BuyerId { get; set; }
    public int SellerId { get; set; }
    public decimal FinalPrice { get; set; }
    public bool Paid { get; set; } = false;
    public bool Shipped { get; set; } = false;
    public string? TrackingNo { get; set; }
    public bool Refused { get; set; } = false;
    public string? ReturnProofUrl { get; set; }

    // Navigation
    public Item Item { get; set; } = null!;
    public User Buyer { get; set; } = null!;
    public User Seller { get; set; } = null!;
}
