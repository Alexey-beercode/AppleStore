using AppleStore.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace AppleStore.Service.Implementations;

public class OrderService : IOrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly ILogger<OrderService> _logger;

    public OrderService(OrderRepository orderRepository, ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<BaseResponse<Order>> GetById(int id)
    {
        var baseResponse = new BaseResponse<Order>();

        var order = (await _orderRepository.GetAll()).FirstOrDefault(x=>x.Id==id);
        if (order == null)
        {
            baseResponse.Description = "Заказ не найден";
            baseResponse.StatusCode = HttpStatusCode.NoContent;
            _logger.LogError("Ошибка получения заказа");
            return baseResponse;
        }

        baseResponse.StatusCode = HttpStatusCode.OK;
        baseResponse.Data = order;
        _logger.LogInformation("Успешное получение заказа");
        return baseResponse;
    }

    public async Task<BaseResponse<Order>> GetByName(string name)
    {
        var baseResponse = new BaseResponse<Order>();

        var order = (await _orderRepository.GetAll()).FirstOrDefault(x=>x.Name==name);
        if (order == null)
        {
            baseResponse.Description = "Заказ не найден";
            baseResponse.StatusCode = HttpStatusCode.NoContent;
            _logger.LogError("Ошибка получения заказа");
            return baseResponse;
        }

        baseResponse.StatusCode = HttpStatusCode.OK;
        baseResponse.Data = order;
        _logger.LogInformation("Успешное получение заказа");
        return baseResponse;
    }

    public async Task<BaseResponse<bool>> CreateOrder(Order orderModel)
    {
        var baseResponse = new BaseResponse<bool>();
        var order = new Order()
        {
            Name = orderModel.Name,
            DeviceId = orderModel.DeviceId,
            Email = orderModel.Email,
            Address = orderModel.Address,
            Price = orderModel.Price,
            Status = orderModel.Status
        };
        await _orderRepository.Create(order);
        baseResponse.StatusCode = HttpStatusCode.OK;
        baseResponse.Data = true;
        _logger.LogInformation("Успешное создание заказа");
        return baseResponse;
    }

    public async Task<BaseResponse<bool>> DeleteOrder(int id)
    {
        var baseResponse = new BaseResponse<bool>();

        var order = (await _orderRepository.GetAll()).FirstOrDefault(x=>x.Id==id);
        if (order == null)
        {
            baseResponse.Description = "Заказ не найден";
            baseResponse.StatusCode = HttpStatusCode.NoContent;
            _logger.LogError("Ошибка получения заказа при попытке удаления заказа");
            return baseResponse;
        }

        await _orderRepository.Delete(order);
        baseResponse.StatusCode = HttpStatusCode.OK;
        baseResponse.Data = true;
        _logger.LogInformation("Успешное удаление заказа");
        return baseResponse;
    }

    public async Task<BaseResponse<IEnumerable<Order>>> GetOrders()
    {
        var baseResponse = new BaseResponse<IEnumerable<Order>>();
        try
        {
            var orders = await _orderRepository.GetAll();
            if (orders.Count == 0)
            {
                baseResponse.Description = "Найдено 0 элементов";
                baseResponse.StatusCode = HttpStatusCode.NoContent;
                _logger.LogError("Ошибка получения заказов");
                return baseResponse;
            }

            baseResponse.Data = orders;
            baseResponse.StatusCode = HttpStatusCode.OK;
            _logger.LogInformation("Успешное получение заказов");
            return baseResponse;
        }
        catch (Exception exception)
        {
            _logger.LogCritical("Возникло исключение при попытке получения заказов");
            return new BaseResponse<IEnumerable<Order>>()
            {
                Description = $"[GetOrders] : {exception.Message}"
            };
        }
    }

    public async Task<BaseResponse<Order>> Edit(Order model)
    {
        var baseResponse = new BaseResponse<Order>();
        try
        {
            var order = (await _orderRepository.GetAll()).FirstOrDefault(x=>x.Id==model.Id);
            if (order == null)
            {
                baseResponse.Description = "Заказ не найден";
                baseResponse.StatusCode = HttpStatusCode.NoContent;
                _logger.LogError("Ошибка получения заказа");
                return baseResponse;
            }

            order.Name = model.Name;
            order.DeviceId = model.DeviceId;
            order.Email = model.Email;
            order.Address = model.Address;
            order.Price = model.Price;
            order.Status = model.Status;
            
            await _orderRepository.Update(order);
            baseResponse.StatusCode = HttpStatusCode.OK;
            _logger.LogInformation("Успешное редактирование заказа");
            return baseResponse;
        }
        catch (Exception e)
        {
            var response = new BaseResponse<Order>();
            response.StatusCode = HttpStatusCode.NotFound;
            response.Description = e.Message;
            _logger.LogCritical("Возникло исключение при попытке редактирования заказа");
            return response;
        }
    }
}