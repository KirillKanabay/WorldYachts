using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class OrderModel:IDataModel<Order>
    {
        public Order LastAddedItem { get; set; }
        public async Task AddAsync(Order item)
        {
            await IsRepeated(item);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await context.Orders.AddAsync(item);
                LastAddedItem = item;
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await Task.Run(() => Load());
        }

        public IEnumerable<Order> Load()
        {
            List<Order> orders = new List<Order>();
            using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var order in context.Orders.Where(i=>!i.IsDeleted))
                {
                    //order.Boat = new BoatModel().GetByIdAsync(order.BoatId);
                    //order.SalesPerson = new SalesPersonModel().GetItemById(order.SalesPersonId);
                    order.Customer = new CustomerModel().GetItemById(order.CustomerId);
                    order.OrderDetails = new OrderDetailsModel().Load().Where(od => od.OrderId == order.Id).ToList();
                    orders.Add(order);
                }
                return orders;
            }
        }

        public async Task DeleteAsync(IEnumerable<Order> removeItems)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                context.Orders.RemoveRange(removeItems);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Order item)
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
                dbOrder.Status = item.Status;
                dbOrder.IsDeleted = item.IsDeleted;

                await context.SaveChangesAsync();
            }
        }

        public async Task IsRepeated(Order item)
        {
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await Task.Run(() => GetItemById(id));
        }

        public Order GetItemById(int id)
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                var item = context.Orders.FirstOrDefault(o => o.Id == id);
                //item.Boat = new BoatModel().GetByIdAsync(item.BoatId);
                //item.SalesPerson = new SalesPersonModel().GetItemById(item.SalesPersonId);
                item.Customer = new CustomerModel().GetItemById(item.CustomerId);
                item.OrderDetails = new OrderDetailsModel().Load().Where(od => od.OrderId == item.Id).ToList();
                return item;
            }
        }
    }
}
