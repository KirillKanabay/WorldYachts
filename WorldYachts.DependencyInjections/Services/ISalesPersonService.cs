using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.Data.ViewModels;

namespace WorldYachts.DependencyInjections.Services
{
    public interface ISalesPersonService
    {
        Task<Data.Entities.SalesPerson> GetByIdAsync(int id);
        Task<IEnumerable<Data.Entities.SalesPerson>> GetAllAsync();
        Task<Data.Entities.SalesPerson> AddAsync(SalesPersonUserViewModel salesPersonUserViewModel);
        Task<Data.Entities.SalesPerson> UpdateAsync(int id, SalesPerson salesPerson);
        Task<Data.Entities.SalesPerson> DeleteAsync(int id);
    }
}
