using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.DependencyInjections.Models
{
    public interface IOrderModel
    {
        event Func<object, Task> OrderModelChanged;
        Task<Order> AddAsync(Order item);
        Task<IEnumerable<Order>> GetAllAsync();
        Task DeleteAsync(Order item);
        Task UpdateAsync(Order item);
        Task<Order> GetByIdAsync(int id);
    }
}
