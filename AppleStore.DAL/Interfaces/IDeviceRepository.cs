using AppleStore.Domain.Entity;

namespace AppleStore.DAL.Interfaces;

public interface IDeviceRepository : IBaseRepository<Device>
{
    Task<Device> GetByName(string? name);
    
}