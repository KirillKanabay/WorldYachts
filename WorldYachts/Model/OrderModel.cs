﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class OrderModel:IDataModel<Order>
    {
        public Order LastAdded { get; set; }
        public async Task AddAsync(Order item)
        {
            await IsRepeated(item);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await context.Orders.AddAsync(item);
                LastAdded = item;
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> LoadAsync()
        {
            return await Task.Run(() => Load());
        }

        public IEnumerable<Order> Load()
        {
            List<Order> orders = new List<Order>();
            using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var order in context.Orders)
                {
                    order.Boat = new BoatModel().Load().FirstOrDefault(b => b.Id == order.BoatId);
                    order.SalesPerson = new SalesPersonModel().Load().FirstOrDefault(sp => sp.Id == order.SalesPersonId);
                    order.Customer = new CustomerModel().Load().FirstOrDefault(c => c.Id == order.CustomerId);
                    order.OrderDetails = new OrderDetailsModel().Load().Where(od => od.OrderId == order.Id).ToList();
                    orders.Add(order);
                }
                return orders;
            }
        }

        public async Task RemoveAsync(IEnumerable<Order> removeItems)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                context.Orders.RemoveRange(removeItems);
                await context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync(Order item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await IsRepeated(item);
                var dbOrder = context.Orders.FirstOrDefault(o => o.Id == item.Id);

                dbOrder.Id = item.Id;
                dbOrder.CustomerId = item.CustomerId;
                dbOrder.SalesPersonId = item.SalesPersonId;
                dbOrder.Date = item.Date;
                dbOrder.BoatId = item.BoatId;
                dbOrder.DeliveryAddress = item.DeliveryAddress;
                dbOrder.City = item.City;
                
                await context.SaveChangesAsync();
            }
        }

        public async Task IsRepeated(Order item)
        {

        }

        public async Task<Order> GetItemByIdAsync(int id)
        {
            return await Task.Run(() => GetItemById(id));
        }

        public Order GetItemById(int id)
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                return context.Orders.FirstOrDefault(o => o.Id == id);
            }
        }
    }
}