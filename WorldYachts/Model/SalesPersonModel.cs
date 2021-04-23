using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class SalesPersonModel:IDataModel<SalesPerson>
    {
        public SalesPerson LastAddedItem { get; set; }
        public async Task AddAsync(SalesPerson item)
        {
            await IsRepeated(item);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await context.SalesPersons.AddAsync(item);
                await context.SaveChangesAsync();
                LastAddedItem = item;
            }
        }

        public async Task<IEnumerable<SalesPerson>> GetAllAsync()
        {
            return await Task.Run(() => Load());
        }

        public IEnumerable<SalesPerson> Load()
        {
            //var spCollection = new List<SalesPerson>();
            //using (var context = WorldYachtsContext.GetDataContext())
            //{
            //    foreach (var sp in context.SalesPersons.Where(i=>!i.IsDeleted))
            //    {
            //        sp.Orders = new OrderModel().Load().Where(o => o.SalesPersonId == sp.Id).ToList();
            //        spCollection.Add(sp);
            //    }
            //}

            return new List<SalesPerson>();
        }

        public async Task DeleteAsync(IEnumerable<SalesPerson> removeItems)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var salesPerson in removeItems)
                {
                    //salesPerson.IsDeleted = true;
                    await UpdateAsync(salesPerson);
                }
            }
        }

        public async Task UpdateAsync(SalesPerson item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await IsRepeated(item);
                var dbSP = context.SalesPersons.FirstOrDefault(sp => sp.Id == item.Id);

                dbSP.FirstName = item.FirstName;
                dbSP.SecondName = item.SecondName;
               // dbSP.IsDeleted = item.IsDeleted;

                await context.SaveChangesAsync();
                LastAddedItem = item;
            }
        }

        public async Task IsRepeated(SalesPerson item)
        {
            
        }

        public async Task<SalesPerson> GetByIdAsync(int id)
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
