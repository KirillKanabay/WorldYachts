using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.DependencyInjections.Models
{
    public interface IContractModel
    {
        event Func<object, Task> ContractModelChanged;
        Task<Contract> AddAsync(Contract item);
        Task<IEnumerable<Contract>> GetAllAsync();
        Task DeleteAsync(Contract item);
        Task UpdateAsync(Contract item);
        Task<Contract> GetByIdAsync(int id);
    }
}
