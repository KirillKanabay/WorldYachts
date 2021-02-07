using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;

namespace WorldYachts.Model
{
    class SalesPersonModel:IDataModel<SalesPerson>
    {
        public SalesPerson LastAdded { get; set; }
        public Task AddAsync(SalesPerson item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SalesPerson>> LoadAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesPerson> Load()
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IEnumerable<SalesPerson> removeItems)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(SalesPerson item)
        {
            throw new NotImplementedException();
        }

        public Task IsRepeated(SalesPerson item)
        {
            throw new NotImplementedException();
        }

        public Task<SalesPerson> GetItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public SalesPerson GetItemById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
