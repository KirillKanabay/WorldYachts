using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.DependencyInjections.Services
{
    public interface IContractService
    {
        Task<Contract> GetByIdAsync(int id);
        Task<IEnumerable<Contract>> GetAllAsync();
        Task<Contract> AddAsync(Contract contract);
        Task<Contract> UpdateAsync(int id, Contract contract);
        Task<Contract> DeleteAsync(int id);
    }
}
