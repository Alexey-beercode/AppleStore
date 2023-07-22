using AppleStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppleStore.DAL;

public class DeviceDbContext: DbContext
{
    public DbSet<Device?> Device { get; set; } = null!;

    public DeviceDbContext(DbContextOptions<DeviceDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}