using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class AccessoryModel:IDataModel<Accessory>
    {
        public Accessory LastAdded { get; set; }

        public async Task AddAsync(Accessory item)
        {
            await IsRepeated(item);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await context.Accessories.AddAsync(item);
                await context.SaveChangesAsync();
                LastAdded = item;
            }
        }

        public async Task<IEnumerable<Accessory>> LoadAsync()
        {
            return await Task.Run(() => Load());
        }

        public IEnumerable<Accessory> Load()
        {
            var accessoryCollection = new List<Accessory>();
            using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var accessory in context.Accessories)
                {
                    accessory.Partner = new PartnerModel().GetItemById(accessory.PartnerId);
                    accessoryCollection.Add(accessory);
                }
            }

            return accessoryCollection;
        }

        public async Task RemoveAsync(IEnumerable<Accessory> removeItems)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                context.Accessories.RemoveRange(removeItems);
                await context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync(Accessory item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await IsRepeated(item);
                var dbAcc = context.Accessories.FirstOrDefault(a => a.Id == item.Id);

                //Копируем измененный аксессуар в БД
                dbAcc.Name = item.Name;
                dbAcc.Description = item.Description;
                dbAcc.Inventory = item.Inventory;
                dbAcc.Price = item.Price;
                dbAcc.Vat = item.Vat;
                dbAcc.OrderLevel = item.OrderLevel;
                dbAcc.OrderBatch = item.OrderBatch;
                dbAcc.PartnerId = item.PartnerId;

                await context.SaveChangesAsync();
            }
        }

        public async Task IsRepeated(Accessory item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                if (context.Accessories.ToList().Any(a => a.CompareTo(item) == 0))
                {
                    throw new ArgumentException("Такой аксессуар уже существует.");
                }
            }
        }

        public async Task<Accessory> GetItemByIdAsync(int id)
        {
            return await Task.Run((() => GetItemById(id)));
        }

        public Accessory GetItemById(int id)
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                return context.Accessories.FirstOrDefault(b => b.Id == id);
            }
        }
    }
}
