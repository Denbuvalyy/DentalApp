using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DentalApp.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlite("Filename=design.db");

        return new AppDbContext(optionsBuilder.Options);
    }
}