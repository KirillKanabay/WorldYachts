using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.DependencyInjections.Services;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class OrderDetailModel:IOrderDetailModel
    {
        public event Func<object, Task> OrderDetailModelChanged;

        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailModel(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        public async Task AddAsync(OrderDetail item)
        {
            await _orderDetailService.AddAsync(item);
            OrderDetailModelChanged?.Invoke(item);
        }

        public async Task<IEnumerable<OrderDetail>> GetAllAsync()
        {
            return await _orderDetailService.GetAllAsync();
        }

        public async Task DeleteAsync(OrderDetail orderDetail)
        {
            await _orderDetailService.DeleteAsync(orderDetail.Id);
            OrderDetailModelChanged?.Invoke(orderDetail);
        }

        public async Task UpdateAsync(OrderDetail item)
        {
            await _orderDetailService.UpdateAsync(item.Id, item);
            OrderDetailModelChanged?.Invoke(item);
        }

        public async Task<OrderDetail> GetByIdAsync(int id)
        {
            return await _orderDetailService.GetByIdAsync(id);
        }
    }
}
