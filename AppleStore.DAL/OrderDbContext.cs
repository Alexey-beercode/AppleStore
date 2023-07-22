using AppleStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppleStore.DAL;

public class OrderDbContext:DbContext
{
    public DbSet<Order?> Order { get; set; } = null!;

    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}