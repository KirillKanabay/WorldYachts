using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.DependencyInjections.Models
{
    public interface IBoatWoodModel
    {
        event Func<object, Task> BoatWoodModelChanged;
        Task AddAsync(BoatWood boatWood);
        Task<IEnumerable<BoatWood>> GetAllAsync();
        Task DeleteAsync(BoatWood boatWood);
        Task UpdateAsync(BoatWood boatWood);
        Task<BoatWood> GetByIdAsync(int id);
    }
}
