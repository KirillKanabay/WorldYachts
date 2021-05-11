using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.DependencyInjections.Services;


namespace WorldYachts.Model
{
    class OrderModel:IOrderModel
    {
        public event Func<object, Task> OrderModelChanged;

        private readonly IOrderService _orderService;

        public OrderModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Order> AddAsync(Order item)
        {
            var order = await _orderService.AddAsync(item);
            OrderModelChanged?.Invoke(item);
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orderService.GetAllAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            await _orderService.DeleteAsync(order.Id);
            OrderModelChanged?.Invoke(order);
        }

        public async Task UpdateAsync(Order item)
        {
            await _orderService.UpdateAsync(item.Id, item);
            OrderModelChanged?.Invoke(item);
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _orderService.GetByIdAsync(id);
        }
    }
}
