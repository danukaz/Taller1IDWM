using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller.Src.Models;

namespace Taller.Src.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(Order order);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
        Task<Order?> GetOrderByIdAsync(int orderId, string userId);
        Task<List<Order>> GetAllOrdersAsync();
    }
}