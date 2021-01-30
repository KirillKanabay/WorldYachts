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
        public async Task AddBoadAsync()
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await IsRepeatedBoat(Boat);
                
                await context.Boats.AddAsync(Boat);
                await context.SaveChangesAsync();
            }
            
        }
        /// <summary>
        /// Асинхронный метод загрузки лодок из БД
        /// </summary>
        /// <returns></returns>
        public async Task<List<Boat>> LoadBoatsAsync()
        {
            return await Task.Run(() => LoadBoats());
        }
        /// <summary>
        /// Синхронный метод загрузки лодок из БД
        /// </summary>
        /// <returns></returns>
        public List<Boat> LoadBoats()
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
        public static async Task RemoveBoatsAsync(IEnumerable<Boat> boats)
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
        public async Task SaveBoatAsync()
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await IsRepeatedBoat(Boat);
                var dbBoat = context.Boats.FirstOrDefault(b => b.Id == Boat.Id);
                
                //Копируем измененую лодку в БД
                dbBoat.Model = Boat.Model;
                dbBoat.Type = Boat.Type;
                dbBoat.NumberOfRowers = Boat.NumberOfRowers;
                dbBoat.Mast = Boat.Mast;
                dbBoat.Color = Boat.Color;
                dbBoat.Wood = Boat.Wood;
                dbBoat.BasePrice = Boat.BasePrice;
                dbBoat.Vat = Boat.Vat;

                await context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Если лодка в БД уже есть, вызывается исключение
        /// </summary>
        /// <param name="boat"></param>
        /// <returns></returns>
        public async Task IsRepeatedBoat(Boat boat)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                if (context.Boats.ToList().Any(c => c.CompareTo(Boat) == 0))
                {
                    throw new ArgumentException("Такая лодка уже существует.");
                }
            }
        }
    }
}
