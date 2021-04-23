using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class AccessoryToBoatModel:IDataModel<AccessoryToBoat>
    {
        public AccessoryToBoat LastAddedItem { get; set; }

        public async Task AddAsync(AccessoryToBoat item)
        {
            await IsRepeated(item);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await context.AccessoryToBoat.AddAsync(item);
                await context.SaveChangesAsync();
                LastAddedItem = item;
            }
        }

        public async Task<IEnumerable<AccessoryToBoat>> GetAllAsync()
        {
            return await Task.Run(() => Load());
        }

        public IEnumerable<AccessoryToBoat> Load()
        {
            var accessoryToBoatCollection = new List<AccessoryToBoat>();
            using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var atb in context.AccessoryToBoat.Where(i=>!i.IsDeleted))
                {
                    atb.Accessory = new AccessoryModel().GetItemById(atb.AccessoryId);
                    //atb.Boat = new BoatModel().GetByIdAsync(atb.BoatId);
                    accessoryToBoatCollection.Add(atb);
                }
            }

            return accessoryToBoatCollection;
        }

        public async Task DeleteAsync(IEnumerable<AccessoryToBoat> removeItems)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var accessoryToBoat in removeItems)
                {
                    accessoryToBoat.IsDeleted = true;
                    await UpdateAsync(accessoryToBoat);
                }
            }
        }

        public async Task UpdateAsync(AccessoryToBoat item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await IsRepeated(item);
                var dbAtb = context.AccessoryToBoat.FirstOrDefault(atb => atb.Id == item.Id);

                //Копируем измененную связь в БД
                dbAtb.AccessoryId = item.AccessoryId;
                dbAtb.BoatId = item.BoatId;
                dbAtb.IsDeleted = item.IsDeleted;

                await context.SaveChangesAsync();
            }
        }

        public async Task IsRepeated(AccessoryToBoat item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                if (context.AccessoryToBoat.ToList().Any(atb => atb.CompareTo(item) == 0))
                {
                    throw new ArgumentException("Такая связь уже существует.");
                }
            }
        }

        public async Task<AccessoryToBoat> GetByIdAsync(int id)
        {
            return await Task.Run((() => GetItemById(id)));
        }

        public AccessoryToBoat GetItemById(int id)
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                return context.AccessoryToBoat.FirstOrDefault(atb => atb.Id == id);
            }
        }
    }
}
