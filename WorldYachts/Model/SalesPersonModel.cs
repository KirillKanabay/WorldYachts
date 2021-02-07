using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class SalesPersonModel:IDataModel<SalesPerson>
    {
        public SalesPerson LastAdded { get; set; }
        public async Task AddAsync(SalesPerson item)
        {
            await IsRepeated(item);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await context.SalesPersons.AddAsync(item);
                await context.SaveChangesAsync();
                LastAdded = item;
            }
        }

        public async Task<IEnumerable<SalesPerson>> LoadAsync()
        {
            return await Task.Run(() => Load());
        }

        public IEnumerable<SalesPerson> Load()
        {
            var spCollection = new List<SalesPerson>();
            using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var sp in context.SalesPersons)
                {
                    sp.Orders = new OrderModel().Load().Where(o => o.SalesPersonId == sp.Id).ToList();
                    spCollection.Add(sp);
                }
            }

            return spCollection;
        }

        public async Task RemoveAsync(IEnumerable<SalesPerson> removeItems)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                context.SalesPersons.RemoveRange(removeItems);
                await context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync(SalesPerson item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await IsRepeated(item);
                var dbSP = context.SalesPersons.FirstOrDefault(sp => sp.Id == item.Id);

                dbSP.Name = item.Name;
                dbSP.SecondName = item.SecondName;

                await context.SaveChangesAsync();
            }
        }

        public async Task IsRepeated(SalesPerson item)
        {
            
        }

        public async Task<SalesPerson> GetItemByIdAsync(int id)
        {
            return await Task.Run((() => GetItemById(id)));
        }

        public SalesPerson GetItemById(int id)
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                return context.SalesPersons.FirstOrDefault(sp => sp.Id == id);
            }
        }
    }
}
