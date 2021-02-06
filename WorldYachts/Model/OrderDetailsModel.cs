using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class OrderDetailsModel:IDataModel<OrderDetails>
    {
        public OrderDetails LastAdded { get; set; }

        public async Task AddAsync(OrderDetails item)
        {
            await IsRepeated(item);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await context.OrderDetails.AddAsync(item);
                LastAdded = item;
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<OrderDetails>> LoadAsync()
        {
            return await Task.Run(() => Load());
        }

        public IEnumerable<OrderDetails> Load()
        {
            List<OrderDetails> orderDetails = new List<OrderDetails>();
            using (var context = WorldYachtsContext.GetDataContext())
            {
                var accessories = context.Accessories;
                var orders = context.Orders;
                foreach (var contextOrderDetails in context.OrderDetails)
                {
                    contextOrderDetails.Accessory = accessories.FirstOrDefault(a => a.Id == contextOrderDetails.AccessoryId);
                    contextOrderDetails.Order = orders.FirstOrDefault(o => o.Id == contextOrderDetails.OrderId);
                    orderDetails.Add(contextOrderDetails);
                }
                return orderDetails;
            }
        }

        public async Task RemoveAsync(IEnumerable<OrderDetails> removeItems)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                context.OrderDetails.RemoveRange(removeItems);
                await context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync(OrderDetails item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await IsRepeated(item);
                var dbOrderDetail = context.OrderDetails.FirstOrDefault(o => o.Id == item.Id);

                dbOrderDetail.Id = item.Id;
                dbOrderDetail.AccessoryId = item.AccessoryId;
                dbOrderDetail.OrderId = item.OrderId;
                
                await context.SaveChangesAsync();
            }
        }

        public async Task IsRepeated(OrderDetails item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                if (context.OrderDetails.ToList().Any(od => od.CompareTo(item) == 0))
                {
                    throw new ArgumentException("Такая лодка уже существует.");
                }
            }
        }

        public async Task<OrderDetails> GetItemByIdAsync(int id)
        {
            return await Task.Run(() => GetItemById(id));
        }

        public OrderDetails GetItemById(int id)
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                return context.OrderDetails.FirstOrDefault(od => od.Id == id);
            }
        }
    }
}
