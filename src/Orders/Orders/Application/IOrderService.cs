using Orders.Domain;
using Orders.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Application
{
    public interface IOrderService 
    {
        Task<IEnumerable<object>> GetAllOrdersAsync();
        Task<object> GetOrderByIdAsync(int id);
        Task<int> CreateOrderAsync(CreateOrderDto order);
        Task<bool> UpdateOrderAsync(UpdateOrderDto order); 
        Task<bool> DeleteOrderAsync(int id);
    }
}
