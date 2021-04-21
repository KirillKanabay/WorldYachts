using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorldYachts.Services.SalesPerson
{
    public interface ISalesPersonService
    {
        Task<Data.Entities.SalesPerson> GetByIdAsync(int id);
    }
}
