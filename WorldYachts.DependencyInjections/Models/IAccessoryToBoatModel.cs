using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.DependencyInjections.Models
{
    public interface IAccessoryToBoatModel
    {
        event Func<object, Task> AccessoryToBoatModelChanged;
        Task AddAsync(AccessoryToBoat item);
        Task<IEnumerable<AccessoryToBoat>> GetAllAsync();
        Task DeleteAsync(AccessoryToBoat item);
        Task UpdateAsync(AccessoryToBoat item);
        Task<AccessoryToBoat> GetByIdAsync(int id);
    }
}
