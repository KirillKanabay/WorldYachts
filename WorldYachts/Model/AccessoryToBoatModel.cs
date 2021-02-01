using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;

namespace WorldYachts.Model
{
    class AccessoryToBoatModel:IDataModel<AccessoryToBoat>
    {
        public Task AddAsync(AccessoryToBoat item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccessoryToBoat>> LoadAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccessoryToBoat> Load()
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IEnumerable<AccessoryToBoat> removeItems)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(AccessoryToBoat item)
        {
            throw new NotImplementedException();
        }

        public Task IsRepeated(AccessoryToBoat item)
        {
            throw new NotImplementedException();
        }

        public Task<AccessoryToBoat> GetItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public AccessoryToBoat GetItemById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
