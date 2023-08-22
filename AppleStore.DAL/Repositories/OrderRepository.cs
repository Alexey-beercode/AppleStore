using AppleStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppleStore.DAL.Repositories;

public class OrderRepository 
{
    public readonly ApplicationDbContext db;

    public OrderRepository(ApplicationDbContext db)
    {
        this.db = db;
    }

    public async Task Create(Order? entity)
    {
        await db.Order.AddAsync(entity);
        await db.SaveChangesAsync();
    }

    public Task<List<Order>> GetAll()
    {
        return db.Order.ToListAsync();
    }
    
    public async Task Delete(Order entity)
    {
        db.Order.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<Order> Update(Order entity)
    {
        db.Order.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }
}