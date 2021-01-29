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

        public async Task AddBoadAsync()
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                if (context.Boats.ToList().Any(c => c.CompareTo(Boat) == 0))
                {
                    throw new ArgumentException("Такая лодка уже существует.");
                }
                await context.Boats.AddAsync(Boat);
                await context.SaveChangesAsync();
            }
            
        }

        public async Task<List<Boat>> LoadBoatsAsync()
        {
            return await Task.Run(() => LoadBoats());
        }

        public List<Boat> LoadBoats()
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                return context.Boats.ToList();
            }
        }

        public static async Task RemoveBoatsAsync(IEnumerable<Boat> boats)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                context.Boats.RemoveRange(boats);
                await context.SaveChangesAsync();
            }
        }

        public async Task SaveBoatAsync()
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                var dbBoat = context.Boats.FirstOrDefault(b => b.Id == Boat.Id);
                
                dbBoat = Boat;

                await context.SaveChangesAsync();
            }
        }
    }
}
