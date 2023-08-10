using AppleStore.DAL.Repositories;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace AppleStore.Service.Implementations;

public class DeviceService : IDeviceService
{
    private readonly DeviceRepository _deviceRepository;
    private readonly ILogger<DeviceService> _logger;
    private readonly IMemoryCache _cache;

    public DeviceService(DeviceRepository deviceRepository, ILogger<DeviceService> logger, IMemoryCache cache)
    {
        _deviceRepository = deviceRepository;
        _logger = logger;
        _cache = cache;
    }

    public async Task<BaseResponse<Device>> GetById(int id)
    {
        var baseResponse = new BaseResponse<Device>();

        var device = (await _deviceRepository.GetAll()).FirstOrDefault(x=>x.Id==id);
        if (device == null)
        {
            baseResponse.Description = "Девайс не найден";
            baseResponse.StatusCode = HttpStatusCode.NoContent;
            _logger.LogError("Ошибка : девайсы не найдены");
            return baseResponse;
        }

        baseResponse.StatusCode = HttpStatusCode.OK;
        baseResponse.Data = device;
        return baseResponse;
    }

    public async Task<BaseResponse<Device>> GetByName(string name)
    {
        var baseResponse = new BaseResponse<Device>();

        var device = (await _deviceRepository.GetAll()).FirstOrDefault(x=>x.Name==name);
        if (device == null)
        {
            baseResponse.Description = "Девайс не найден";
            baseResponse.StatusCode = HttpStatusCode.NoContent;
            _logger.LogError("Ошибка : девайсы не найдены");
            return baseResponse;
        }

        baseResponse.StatusCode = HttpStatusCode.OK;
        baseResponse.Data = device;
        _logger.LogInformation("Успешное получение девайса");
        return baseResponse;
    }

    public async Task<BaseResponse<bool>> CreateDevice(Device model)
    {
        var baseResponse = new BaseResponse<bool>();
        var device = new Device()
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Type = (DeviceType)Convert.ToInt32(model.Type)
        };
        await _deviceRepository.Create(device);
        baseResponse.StatusCode = HttpStatusCode.OK;
        baseResponse.Data = true;
        _logger.LogInformation("Успешное создание девайса");
        return baseResponse;
    }

    public async Task<BaseResponse<bool>> DeleteDevice(int id)
    {
        var baseResponse = new BaseResponse<bool>();

        var device = (await _deviceRepository.GetAll()).FirstOrDefault(x=>x.Id==id);
        if (device == null)
        {
            baseResponse.Description = "Девайс не найден";
            baseResponse.StatusCode = HttpStatusCode.NoContent;
            _logger.LogError("Ошибка : девайсы не найдены");
            return baseResponse;
        }

        await _deviceRepository.Delete(device);
        baseResponse.StatusCode = HttpStatusCode.OK;
        baseResponse.Data = true;
        _logger.LogInformation("Успешное удаление девайса");
        return baseResponse;
    }

    public async Task<BaseResponse<IEnumerable<Device>>> GetDevices(bool useCache)
    {
        var baseResponse = new BaseResponse<IEnumerable<Device>>();
        
        if (useCache && _cache.TryGetValue("AllDevices", out IEnumerable<Device>? devicesFromCache))
        {
            _logger.LogInformation("Получение всех девайсов из кэша");
            baseResponse.Data = devicesFromCache;
            baseResponse.StatusCode = HttpStatusCode.OK;
            return baseResponse;
        }

        try
        {
            var devices = await _deviceRepository.GetAll();
            if (devices.Count == 0)
            {
                baseResponse.Description = "Найдено 0 элементов";
                baseResponse.StatusCode = HttpStatusCode.NoContent;
                _logger.LogError("Ошибка : девайсы не найдены");
                return baseResponse;
            }

            baseResponse.Data = devices;
            baseResponse.StatusCode = HttpStatusCode.OK;
            
            if (useCache)
            {
                _cache.Set("AllDevices", devices,
                    new MemoryCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) });
                _logger.LogInformation("Все девайсы добавлены в кэш");
            }
            return baseResponse;
        }
        catch (Exception exception)
        {
            _logger.LogCritical("Возникло исключение при получении всех девайсов");
            return new BaseResponse<IEnumerable<Device>>()
            {
                Description = $"[GetDevices] : {exception.Message}"
            };
        }
    }


    public async Task<BaseResponse<Device>> Edit(Device model)
    {
        var baseResponse = new BaseResponse<Device>();
        try
        {
            var device = (await _deviceRepository.GetAll()).FirstOrDefault(x=>x.Id==model.Id);
            if (device == null)
            {
                baseResponse.Description = "Девайс не найден";
                baseResponse.StatusCode = HttpStatusCode.NoContent;
                _logger.LogError("Ошибка : девайс для редактирования не найдены");
                return baseResponse;
            }

            device.Description = model.Description;
            device.Name = model.Name;
            device.Price = model.Price;
            device.Type = (DeviceType)Convert.ToInt32(model.Type);
            await _deviceRepository.Update(device);
            baseResponse.StatusCode = HttpStatusCode.OK;
            _logger.LogInformation("Успешное редактирование девайса");
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