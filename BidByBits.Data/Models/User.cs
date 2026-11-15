namespace BidByBits.Data.Models;

public enum UserRole { Buyer, Seller }
public enum ItemStatus { Active, Sold, Expired, Cancelled }
public enum ItemType { Auction, FixedPrice }

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public decimal AverageScore { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    // Navigation
    public ICollection<Item> OwnedItems { get; set; } = new List<Item>();
    public ICollection<Bid> Bids { get; set; } = new List<Bid>();
    public ICollection<Order> PurchasedOrders { get; set; } = new List<Order>();
    public ICollection<Order> SoldOrders { get; set; } = new List<Order>();
    public ICollection<CredibilityLog> CredibilityLogsAsBuyer { get; set; } = new List<CredibilityLog>();
    public ICollection<Rating> GivenRatings { get; set; } = new List<Rating>();
    public ICollection<Rating> ReceivedRatings { get; set; } = new List<Rating>();
}
