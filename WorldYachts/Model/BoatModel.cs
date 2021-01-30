using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class BoatModel
    {
        public Boat Boat { get; set; }

        public BoatModel()
        {

        }

        public BoatModel(Boat boat)
        {
            Boat = boat;
        }
        /// <summary>
        /// Асинхронный метод добавления лодки в БД
        /// </summary>
        /// <returns></returns>
        public static async Task AddAsync(Boat boat)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await IsRepeated(boat);
                
                await context.Boats.AddAsync(boat);
                await context.SaveChangesAsync();
            }
            
        }
        /// <summary>
        /// Асинхронный метод загрузки лодок из БД
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Boat>> LoadAsync()
        {
            return await Task.Run(() => Load());
        }
        /// <summary>
        /// Синхронный метод загрузки лодок из БД
        /// </summary>
        /// <returns></returns>
        public static List<Boat> Load()
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                return context.Boats.ToList();
            }
        }
        /// <summary>
        /// Асинхронный метод удаления лодок из БД
        /// </summary>
        /// <param name="boats"></param>
        /// <returns></returns>
        public static async Task RemoveAsync(IEnumerable<Boat> boats)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                context.Boats.RemoveRange(boats);
                await context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Сохранение измененной лодки в БД
        /// </summary>
        /// <returns></returns>
        public static async Task SaveAsync(Boat boat)
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

                await context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Если лодка в БД уже есть, вызывается исключение
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
