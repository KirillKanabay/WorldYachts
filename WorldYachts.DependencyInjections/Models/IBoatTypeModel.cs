using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.DependencyInjections.Models
{
    public interface IBoatTypeModel
    {
        event Func<object, Task> BoatTypeModelChanged;
        Task AddAsync(BoatType boatType);
        Task<IEnumerable<BoatType>> GetAllAsync();
        Task DeleteAsync(BoatType boatType);
        Task UpdateAsync(BoatType boatType);
        Task<BoatType> GetByIdAsync(int id);
    }
}
