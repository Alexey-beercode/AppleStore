using AppleStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppleStore.DAL.Repositories;

public class DeviceRepository
{
    public readonly ApplicationDbContext db;

    public DeviceRepository(ApplicationDbContext db)
    {
        this.db = db;
    }

    public async Task Create(Device? entity)
    {
        await db.Devices.AddAsync(entity);
        await db.SaveChangesAsync();
    }

    public Task<List<Device?>> GetAll()
    {
        return db.Devices.ToListAsync();
    }

    public async Task Delete(Device? entity)
    {
        db.Devices.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<Device> Update(Device entity)
    {
        db.Devices.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }
}