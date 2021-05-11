using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.DependencyInjections.Services
{
    public interface IOrderDetailService
    {
        Task<OrderDetail> GetByIdAsync(int id);
        Task<IEnumerable<OrderDetail>> GetAllAsync();
        Task<OrderDetail> AddAsync(OrderDetail orderDetail);
        Task<OrderDetail> UpdateAsync(int id, OrderDetail orderDetail);
        Task<OrderDetail> DeleteAsync(int id);
    }
}
