namespace AppleStore.Service.Implementations;

public class DeviceService:IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;

    public DeviceService(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }

    public async Task<BaseResponse<Device>> GetById(int id)
    {
        var baseResponse = new BaseResponse<Device>();
        
            var device = await _deviceRepository.GetById(id);
            if (device==null)
            {
                baseResponse.Description = "Девайс не найден";
                baseResponse.StatusCode = HttpStatusCode.NoContent;
                return baseResponse;
            }

            baseResponse.StatusCode = HttpStatusCode.OK;
            baseResponse.Data = device;
            return baseResponse;
    }
    
    public async Task<BaseResponse<Device>> GetByName(string name)
    {
        var baseResponse = new BaseResponse<Device>();
        
        var device = await _deviceRepository.GetByName(name);
        if (device==null)
        {
            baseResponse.Description = "Девайс не найден";
            baseResponse.StatusCode = HttpStatusCode.NoContent;
            return baseResponse;
        }

        baseResponse.StatusCode = HttpStatusCode.OK;
        baseResponse.Data = device;
        return baseResponse;
    }
    
    public async Task<BaseResponse<bool>> CreateDevice(DeviceViewModel model)
    {
        var baseResponse = new BaseResponse<bool>();
        var device = new Device()
        {
            Model = model.Model,
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            ReleaseDate = model.ReleaseDate,
            Type = (DeviceType)Convert.ToInt32(model.Type)
        };
        await _deviceRepository.Create(device);
        baseResponse.StatusCode = HttpStatusCode.OK;
        baseResponse.Data = true;
        return baseResponse;
    }
    
    public async Task<BaseResponse<bool>> DeleteDevice(int id)
    {
        var baseResponse = new BaseResponse<bool>();
        
        var device = await _deviceRepository.GetById(id);
        if (device==null)
        {
            baseResponse.Description = "Девайс не найден";
            baseResponse.StatusCode = HttpStatusCode.NoContent;
            return baseResponse;
        }

        await _deviceRepository.Delete(device);
        baseResponse.StatusCode = HttpStatusCode.OK;
        baseResponse.Data = true;
        return baseResponse;
    }
    
    public async Task<BaseResponse<IEnumerable<Device>>> GetDevices()
    {
        var baseResponse = new BaseResponse<IEnumerable<Device>>();
        try
        {
            var devices=await _deviceRepository.Select();
            if (devices.Count == 0)
            {
                baseResponse.Description = "Найдено 0 элементов";
                baseResponse.StatusCode = HttpStatusCode.NoContent;
                return baseResponse;
            }
            
            baseResponse.Data = devices;
            baseResponse.StatusCode = HttpStatusCode.OK;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<IEnumerable<Device>>()
            {
                Description = $"[GetDevices] : {exception.Message}"
            };
        }
    }

    public async Task<BaseResponse<Device>> Edit(DeviceViewModel model)
    {
        var baseResponse = new BaseResponse<Device>();
        try
        {
            var device = await _deviceRepository.GetById(model.Id);
            if (device==null)
            {
                baseResponse.Description = "Девайс не найден";
                baseResponse.StatusCode = HttpStatusCode.NoContent;
                return baseResponse;
            }

            device.Description = model.Description;
            device.Name = model.Name;
            device.ReleaseDate = model.ReleaseDate;
            device.Model = model.Model;
            device.Price = model.Price;
            device.Type = (DeviceType)Convert.ToInt32(model.Type);
            await _deviceRepository.Update(device);
            baseResponse.StatusCode = HttpStatusCode.OK;
            return baseResponse;
        }
        catch (Exception e)
        {
            var response = new BaseResponse<Device>();
            response.StatusCode = HttpStatusCode.NotFound;
            response.Description = e.Message;
            return response;
        }
    }
}