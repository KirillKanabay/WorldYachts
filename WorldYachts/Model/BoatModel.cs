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
        private Boat _boat;

        public BoatModel()
        {

        }

        public BoatModel(Boat boat)
        {
            _boat = boat;
        }

        public async Task AddBoadAsync()
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                if (context.Boats.ToList().Any(c => c.CompareTo(_boat) == 0))
                {
                    throw new ArgumentException("Такая лодка уже существует.");
                }
                await context.Boats.AddAsync(_boat);
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
    }
}
