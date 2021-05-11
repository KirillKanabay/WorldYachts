using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Model
{
    class ContractModel:IContractModel
    {
        public event Func<object, Task> ContractModelChanged;

        private readonly IContractService _contractService;

        public ContractModel(IContractService contractService)
        {
            _contractService = contractService;
        }

        public async Task<Contract> AddAsync(Contract item)
        {
            var contract = await _contractService.AddAsync(item);
            ContractModelChanged?.Invoke(item);
            return contract;
        }

        public async Task<IEnumerable<Contract>> GetAllAsync()
        {
            return await _contractService.GetAllAsync();
        }

        public async Task DeleteAsync(Contract contract)
        {
            await _contractService.DeleteAsync(contract.Id);
            ContractModelChanged?.Invoke(contract);
        }

        public async Task UpdateAsync(Contract item)
        {
            await _contractService.UpdateAsync(item.Id, item);
            ContractModelChanged?.Invoke(item);
        }

        public async Task<Contract> GetByIdAsync(int id)
        {
            return await _contractService.GetByIdAsync(id);
        }
    }
}
