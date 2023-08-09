using AppleStore.Domain.Entity;
using AppleStore.Domain.Response;

namespace AppleStore.Service.Interfaces;

public interface IDeviceService
{
    Task<BaseResponse<IEnumerable<Device>>> GetDevices(bool useCache);

    Task<BaseResponse<Device>> GetById(int id);

    Task<BaseResponse<Device>> GetByName(string name);

    Task<BaseResponse<bool>> CreateDevice(Device model);

    Task<BaseResponse<bool>> DeleteDevice(int id);

    Task<BaseResponse<Device>> Edit(Device model);
}