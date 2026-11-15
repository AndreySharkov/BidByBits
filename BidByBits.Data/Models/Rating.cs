namespace BidByBits.Data.Models;

public class Rating
{
    public int Id { get; set; }
    public int FromUserId { get; set; }
    public int ToUserId { get; set; }
    public int Score { get; set; } // 1-5
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User FromUser { get; set; } = null!;
    public User ToUser { get; set; } = null!;
}
