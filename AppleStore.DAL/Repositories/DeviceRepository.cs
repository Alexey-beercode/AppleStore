using AppleStore.DAL.Interfaces;
using AppleStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppleStore.DAL.Repositories;

public class DeviceRepository : IDeviceRepository
{
    public readonly DeviceDbContext db;

    public DeviceRepository(DeviceDbContext db)
    {
        this.db = db;
    }

    public async Task<bool> Create(Device? entity)
    {
        await db.Device.AddAsync(entity);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<Device?> GetById(int? id)
    {
        return await db.Device.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Device?>> Select()
    {
        return db.Device.ToListAsync();
    }

    public async Task<bool> Delete(Device? entity)
    {
        db.Device.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<Device> Update(Device entity)
    {
        db.Device.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Device> GetByName(string? name)
    {
        return await db.Device.FirstOrDefaultAsync(x => x.Name == name);
    }
}