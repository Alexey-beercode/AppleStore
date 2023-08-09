using AppleStore.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppleStore.DAL;

public class DeviceDbContext: DbContext
{
    public DbSet<Device> Device { get; set; } = null!;

    public DeviceDbContext(DbContextOptions<DeviceDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "44546e06-8719-4ad8-b88a-f271ae9d6eab",
            Name = "admin",
            NormalizedName = "ADMIN"
        });
    }
}