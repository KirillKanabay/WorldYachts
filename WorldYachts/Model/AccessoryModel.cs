using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;

namespace WorldYachts.Model
{
    class AccessoryModel:IDataModel<Accessory>
    {
        public async Task AddAsync(Accessory item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Accessory>> LoadAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Accessory> Load()
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IEnumerable<Accessory> removeItems)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Accessory item)
        {
            throw new NotImplementedException();
        }

        public Task IsRepeated(Accessory item)
        {
            throw new NotImplementedException();
        }

        public Task<Accessory> GetItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Accessory GetItemById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
