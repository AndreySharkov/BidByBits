using BidByBits.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BidByBits.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Bid> Bids => Set<Bid>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<CredibilityLog> CredibilityLogs => Set<CredibilityLog>();
    public DbSet<Rating> Ratings => Set<Rating>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Users
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.PasswordHash).HasMaxLength(512);
            entity.Property(e => e.DisplayName).HasMaxLength(100);
        });

        // Categories (self-referencing)
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.HasOne(c => c.Parent)
                  .WithMany(c => c.SubCategories)
                  .HasForeignKey(c => c.ParentId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Items
        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.ImageUrl).HasMaxLength(500);

            entity.HasOne(i => i.Owner)
                  .WithMany(u => u.OwnedItems)
                  .HasForeignKey(i => i.OwnerId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(i => i.Category)
                  .WithMany(c => c.Items)
                  .HasForeignKey(i => i.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Bids
        modelBuilder.Entity<Bid>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(b => b.Item)
                  .WithMany(i => i.Bids)
                  .HasForeignKey(b => b.ItemId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(b => b.User)
                  .WithMany(u => u.Bids)
                  .HasForeignKey(b => b.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Orders
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(o => o.Item)
                  .WithMany()
                  .HasForeignKey(o => o.ItemId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(o => o.Buyer)
                  .WithMany(u => u.PurchasedOrders)
                  .HasForeignKey(o => o.BuyerId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(o => o.Seller)
                  .WithMany(u => u.SoldOrders)
                  .HasForeignKey(o => o.SellerId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // CredibilityLogs
        modelBuilder.Entity<CredibilityLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(cl => cl.Order)
                  .WithOne()
                  .HasForeignKey<CredibilityLog>(cl => cl.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Ratings
        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(r => r.FromUser)
                  .WithMany(u => u.GivenRatings)
                  .HasForeignKey(r => r.FromUserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.ToUser)
                  .WithMany(u => u.ReceivedRatings)
                  .HasForeignKey(r => r.ToUserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.Score).HasDefaultValue(5);
        });
    }
}
