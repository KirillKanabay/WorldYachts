using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.Services.ViewModels;

namespace WorldYachts.DependencyInjections.Models
{
    public interface ISalesPersonModel
    {
        event Func<object, Task> SalesPersonModelChanged;
        Task AddAsync(SalesPersonUserViewModel salesPersonUserViewModel);
        Task<IEnumerable<SalesPerson>> GetAllAsync();
        Task DeleteAsync(SalesPersonUserViewModel salesPersonUserViewModel);
        Task UpdateAsync(SalesPersonUserViewModel salesPersonUserViewModel);
        Task<BoatWood> GetByIdAsync(int id);
    }
}
