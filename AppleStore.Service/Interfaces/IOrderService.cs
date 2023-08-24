namespace AppleStore.Service.Interfaces;

public interface IOrderService
{
    Task<BaseResponse<IEnumerable<Order>>> GetOrders(bool useCache);

    Task<BaseResponse<Order>> GetById(int id);

    Task<BaseResponse<Order>> GetByName(string name);

    Task<BaseResponse<bool>> CreateOrder(Order order);

    Task<BaseResponse<bool>> DeleteOrder(int id);

    Task<BaseResponse<Order>> Edit(Order model);
}