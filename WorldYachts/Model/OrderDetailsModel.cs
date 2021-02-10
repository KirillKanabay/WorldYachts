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
        public OrderDetails LastAddedItem { get; set; }

        public async Task AddAsync(OrderDetails item)
        {
            await IsRepeated(item);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await context.OrderDetails.AddAsync(item);
                LastAddedItem = item;
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<OrderDetails>> LoadAsync()
        {
            return await Task.Run(() => Load());
        }

        public IEnumerable<OrderDetails> Load()
        {
            List<OrderDetails> odCollection = new List<OrderDetails>();
            using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var orderDetails in context.OrderDetails.Where(i=>!i.IsDeleted))
                {
                    orderDetails.Accessory = new AccessoryModel().GetItemById(orderDetails.AccessoryId);
                    orderDetails.Order = new OrderModel().GetItemById(orderDetails.OrderId);
                    odCollection.Add(orderDetails);
                }
                return odCollection;
            }
        }

        public async Task RemoveAsync(IEnumerable<OrderDetails> removeItems)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var orderDetails in removeItems)
                {
                    orderDetails.IsDeleted = true;
                    await SaveAsync(orderDetails);
                }
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
                dbOrderDetail.IsDeleted = item.IsDeleted;

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
