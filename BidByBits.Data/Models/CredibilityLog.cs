namespace BidByBits.Data.Models;

public class CredibilityLog
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public bool Refused { get; set; }
    public bool ProofGiven { get; set; } = false;
    public bool Resolved { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Order Order { get; set; } = null!;
}
