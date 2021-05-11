using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.DependencyInjections.Services
{
    public interface IOrderService
    {
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> AddAsync(Order order);
        Task<Order> UpdateAsync(int id, Order order);
        Task<Order> DeleteAsync(int id);
    }
}
