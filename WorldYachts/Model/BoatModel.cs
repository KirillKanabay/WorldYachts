using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class BoatModel:IDataModel<Boat>
    {
        public Boat LastAddedItem { get; set; }

        /// <summary>
        /// Асинхронный метод добавления лодки в БД
        /// </summary>
        /// <returns></returns>
        public async Task AddAsync(Boat boat)
        {
            await IsRepeated(boat);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await context.Boats.AddAsync(boat);
                await context.SaveChangesAsync();
                LastAddedItem = boat;
            }
            
        }
        /// <summary>
        /// Асинхронный метод загрузки лодок из БД
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Boat>> LoadAsync()
        {
            return await Task.Run(() => Load());
        }
        /// <summary>
        /// Синхронный метод загрузки лодок из БД
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Boat> Load()
        {
            List<Boat> boats = new List<Boat>();
            using (var context = WorldYachtsContext.GetDataContext())
            {
                var atb = context.AccessoryToBoat;
                foreach (var contextBoat in context.Boats.Where(i=>!i.IsDeleted))
                {
                    contextBoat.AccessoryToBoat = new AccessoryToBoatModel().Load()
                        .Where(i=>i.Boat.Model == contextBoat.Model).ToList();
                    boats.Add(contextBoat);
                }
                return boats;
            }
        }

        /// <summary>
        /// Асинхронный метод удаления коллекции лодок из БД
        /// </summary>
        /// <param name="removeBoats">Коллекция лодок</param>
        /// <returns></returns>
        public async Task RemoveAsync(IEnumerable<Boat> removeBoats)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var removeBoat in removeBoats)
                {
                    removeBoat.IsDeleted = true;
                    await SaveAsync(removeBoat);
                }
            }
        }

        
        /// <summary>
        /// Сохранение измененной лодки в БД
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync(Boat boat)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await IsRepeated(boat);
                var dbBoat = context.Boats.FirstOrDefault(b => b.Id == boat.Id);
                
                //Копируем измененую лодку в БД
                dbBoat.Model = boat.Model;
                dbBoat.Type = boat.Type;
                dbBoat.NumberOfRowers = boat.NumberOfRowers;
                dbBoat.Mast = boat.Mast;
                dbBoat.Color = boat.Color;
                dbBoat.Wood = boat.Wood;
                dbBoat.BasePrice = boat.BasePrice;
                dbBoat.Vat = boat.Vat;
                dbBoat.IsDeleted = boat.IsDeleted;

                await context.SaveChangesAsync();
            }
        }

        Task IDataModel<Boat>.IsRepeated(Boat item)
        {
            return IsRepeated(item);
        }

        public async Task<Boat> GetItemByIdAsync(int id)
        {
            return await Task.Run(() => GetItemById(id));
        }

        public Boat GetItemById(int id)
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                return context.Boats.FirstOrDefault(b => b.Id == id);
            }
        }

        /// <summary>
        /// Проверка идентичной лодки в БД
        /// </summary>
        /// <param name="boat"></param>
        /// <returns></returns>
        public static async Task IsRepeated(Boat boat)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                if (context.Boats.ToList().Any(c => c.CompareTo(boat) == 0))
                {
                    throw new ArgumentException("Такая лодка уже существует.");
                }
            }
        }
    }
}
