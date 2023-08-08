using AppleStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppleStore.DAL.Repositories;

public class DeviceRepository
{
    public readonly DeviceDbContext db;

    public DeviceRepository(DeviceDbContext db)
    {
        this.db = db;
    }

    public async Task Create(Device? entity)
    {
        await db.Device.AddAsync(entity);
        await db.SaveChangesAsync();
    }

    public Task<List<Device?>> GetAll()
    {
        return db.Device.ToListAsync();
    }

    public async Task Delete(Device? entity)
    {
        db.Device.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<Device> Update(Device entity)
    {
        db.Device.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }
}