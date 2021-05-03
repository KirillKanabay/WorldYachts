using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class SalesPersonModel:ISalesPersonModel
    {
        public event Func<object, Task> SalesPersonModelChanged;

        public Task AddAsync(BoatWood boatWood)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BoatWood>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(BoatWood boatWood)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(BoatWood boatWood)
        {
            throw new NotImplementedException();
        }

        public Task<BoatWood> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
