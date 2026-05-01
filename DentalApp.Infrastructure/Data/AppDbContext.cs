using DentalApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<VisitPhoto> VisitPhotos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}