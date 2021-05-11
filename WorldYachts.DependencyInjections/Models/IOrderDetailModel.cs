using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.DependencyInjections.Models
{
    public interface IOrderDetailModel
    {
        event Func<object, Task> OrderDetailModelChanged;
        Task AddAsync(OrderDetail item);
        Task<IEnumerable<OrderDetail>> GetAllAsync();
        Task DeleteAsync(OrderDetail item);
        Task UpdateAsync(OrderDetail item);
        Task<OrderDetail> GetByIdAsync(int id);
    }
}
