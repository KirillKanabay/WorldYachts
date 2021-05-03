using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldYachts.DependencyInjections.Services
{
    public interface IAccessoryToBoatService
    {
        Task<Data.Entities.AccessoryToBoat> GetByIdAsync(int id);
        Task<IEnumerable<Data.Entities.AccessoryToBoat>> GetAllAsync();
        Task<Data.Entities.AccessoryToBoat> AddAsync(Data.Entities.AccessoryToBoat accessoryToBoat);
        Task<Data.Entities.AccessoryToBoat> UpdateAsync(int id, Data.Entities.AccessoryToBoat accessoryToBoat);
        Task<Data.Entities.AccessoryToBoat> DeleteAsync(int id);
    }
}
