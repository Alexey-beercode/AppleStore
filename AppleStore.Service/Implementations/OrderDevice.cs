using AppleStore.DAL.Repositories;

namespace AppleStore.Service.Implementations;

public class OrderService : IOrderService
{
    private readonly OrderRepository _orderRepository;

    public OrderService(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<BaseResponse<Order>> GetById(int id)
    {
        var baseResponse = new BaseResponse<Order>();

        var order = await _orderRepository.GetById(id);
        if (order == null)
        {
            baseResponse.Description = "Заказ не найден";
            baseResponse.StatusCode = HttpStatusCode.NoContent;
            return baseResponse;
        }

        baseResponse.StatusCode = HttpStatusCode.OK;
        baseResponse.Data = order;
        return baseResponse;
    }

    public async Task<BaseResponse<Order>> GetByName(string name)
    {
        var baseResponse = new BaseResponse<Order>();

        var order = await _orderRepository.GetByName(name);
        if (order == null)
        {
            baseResponse.Description = "Заказ не найден";
            baseResponse.StatusCode = HttpStatusCode.NoContent;
            return baseResponse;
        }

        baseResponse.StatusCode = HttpStatusCode.OK;
        baseResponse.Data = order;
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
            Address = orderModel.Address
        };
        await _orderRepository.Create(order);
        baseResponse.StatusCode = HttpStatusCode.OK;
        baseResponse.Data = true;
        return baseResponse;
    }

    public async Task<BaseResponse<bool>> DeleteOrder(int id)
    {
        var baseResponse = new BaseResponse<bool>();

        var order = await _orderRepository.GetById(id);
        if (order == null)
        {
            baseResponse.Description = "Заказ не найден";
            baseResponse.StatusCode = HttpStatusCode.NoContent;
            return baseResponse;
        }

        await _orderRepository.Delete(order);
        baseResponse.StatusCode = HttpStatusCode.OK;
        baseResponse.Data = true;
        return baseResponse;
    }

    public async Task<BaseResponse<IEnumerable<Order>>> GetOrders()
    {
        var baseResponse = new BaseResponse<IEnumerable<Order>>();
        try
        {
            var orders = await _orderRepository.Select();
            if (orders.Count == 0)
            {
                baseResponse.Description = "Найдено 0 элементов";
                baseResponse.StatusCode = HttpStatusCode.NoContent;
                return baseResponse;
            }

            baseResponse.Data = orders;
            baseResponse.StatusCode = HttpStatusCode.OK;

            return baseResponse;
        }
        catch (Exception exception)
        {
            return new BaseResponse<IEnumerable<Order>>()
            {
                Description = $"[GetOrders] : {exception.Message}"
            };
        }
    }

    public async Task<BaseResponse<Order>> Edit(Order orderModel)
    {
        var baseResponse = new BaseResponse<Order>();
        try
        {
            var order = await _orderRepository.GetById(orderModel.Id);
            if (order == null)
            {
                baseResponse.Description = "Заказ не найден";
                baseResponse.StatusCode = HttpStatusCode.NoContent;
                return baseResponse;
            }

            order.Name = orderModel.Name;
            order.DeviceId = orderModel.DeviceId;
            order.Email = orderModel.Email;
            order.Address = orderModel.Address;
            await _orderRepository.Update(order);
            baseResponse.StatusCode = HttpStatusCode.OK;
            return baseResponse;
        }
        catch (Exception e)
        {
            var response = new BaseResponse<Order>();
            response.StatusCode = HttpStatusCode.NotFound;
            response.Description = e.Message;
            return response;
        }
    }
}