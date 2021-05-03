using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.Data.ViewModels;

namespace WorldYachts.DependencyInjections.Models
{
    public interface ISalesPersonModel
    {
        event Func<object, Task> SalesPersonModelChanged;
        Task AddAsync(SalesPersonUserViewModel salesPersonUserViewModel);
        Task<IEnumerable<SalesPerson>> GetAllAsync();
        Task DeleteAsync(SalesPerson salesPerson);
        Task UpdateAsync(SalesPerson salesPerson);
        Task<SalesPerson> GetByIdAsync(int id);
    }
}
