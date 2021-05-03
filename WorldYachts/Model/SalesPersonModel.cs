using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.Data.ViewModels;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Model
{
    class SalesPersonModel:ISalesPersonModel
    {
        public event Func<object, Task> SalesPersonModelChanged;

        private readonly ISalesPersonService _salesPersonService;
        public SalesPersonModel(ISalesPersonService salesPersonService)
        {
            _salesPersonService = salesPersonService;
        }

        public async Task AddAsync(SalesPersonUserViewModel salesPersonUserViewModel)
        {
            await _salesPersonService.AddAsync(salesPersonUserViewModel);
            SalesPersonModelChanged?.Invoke(salesPersonUserViewModel);
        }

        public async Task<IEnumerable<SalesPerson>> GetAllAsync()
        {
            return await _salesPersonService.GetAllAsync();
        }

        public async Task DeleteAsync(SalesPerson salesPerson)
        {
            await _salesPersonService.DeleteAsync(salesPerson.Id);
            SalesPersonModelChanged?.Invoke(salesPerson);
        }

        public async Task UpdateAsync(SalesPerson salesPerson)
        {
            await _salesPersonService.UpdateAsync(salesPerson.Id, salesPerson);
            SalesPersonModelChanged?.Invoke(salesPerson);
        }

        public async Task<SalesPerson> GetByIdAsync(int id)
        {
            return await _salesPersonService.GetByIdAsync(id);
        }
    }
}
