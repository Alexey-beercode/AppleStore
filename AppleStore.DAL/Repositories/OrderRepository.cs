﻿using AppleStore.DAL.Interfaces;
using AppleStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppleStore.DAL.Repositories;

public class OrderRepository
{
    public readonly OrderDbContext db;

    public OrderRepository(OrderDbContext db)
    {
        this.db = db;
    }

    public async Task<bool> Create(Order? entity)
    {
        await db.Order.AddAsync(entity);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<Order?> GetById(int? id)
    {
        return await db.Order.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Order?>> Select()
    {
        return db.Order.ToListAsync();
    }
    
    public async Task<bool> Delete(Order entity)
    {
        db.Order.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<Order> Update(Order entity)
    {
        db.Order.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Order> GetByName(string? name)
    {
        return await db.Order.FirstOrDefaultAsync(x => x.Name==name);
    }
}