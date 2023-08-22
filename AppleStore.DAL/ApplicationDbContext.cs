using AppleStore.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppleStore.DAL;

public class ApplicationDbContext: IdentityDbContext<IdentityUser>
{
    public DbSet<Device> Device { get; set; } = null!;
    public DbSet<Order> Order { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}