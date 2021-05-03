using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Services.Models;
using WorldYachts.Services.ViewModels;

namespace WorldYachts.Services.SalesPerson
{
    public interface ISalesPersonService
    {
        Task<Data.Entities.SalesPerson> GetByIdAsync(int id);
        Task<IEnumerable<Data.Entities.SalesPerson>> GetAllAsync();
        Task<Data.Entities.SalesPerson> AddAsync(SalesPersonUserViewModel salesPersonUserViewModel);
        Task<Data.Entities.SalesPerson> UpdateAsync(int id, SalesPersonUserViewModel salesPersonModel);
        Task<Data.Entities.SalesPerson> DeleteAsync(int id);
    }
}
