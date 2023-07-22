using AppleStore.Domain.Entity;
using AppleStore.Domain.Response;
using AppleStore.Domain.ViewModels;

namespace AppleStore.Service.Interfaces;

public interface IDeviceService
{
    Task<BaseResponse<IEnumerable<Device>>> GetDevices();

    Task<BaseResponse<Device>> GetById(int id);

    Task<BaseResponse<Device>> GetByName(string name);

    Task<BaseResponse<bool>> CreateDevice(DeviceViewModel model);

    Task<BaseResponse<bool>> DeleteDevice(int id);

    Task<BaseResponse<Device>> Edit(DeviceViewModel model);
}