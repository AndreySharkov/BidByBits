using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BidByBits.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        // Use SQLite for development/migrations (easy to set up, no server needed)
        optionsBuilder.UseSqlite("Data Source=bidbybits.db");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
