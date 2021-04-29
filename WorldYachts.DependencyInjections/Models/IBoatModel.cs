using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.DependencyInjections.Models
{
    public interface IBoatModel
    {
        event Func<object, Task> BoatModelChanged;

        Task AddAsync(Boat boat);
        Task<IEnumerable<Boat>> GetAllAsync();
        Task DeleteAsync(Boat boat);
        Task UpdateAsync(Boat boat);
        Task<Boat> GetByIdAsync(int id);
    }
}