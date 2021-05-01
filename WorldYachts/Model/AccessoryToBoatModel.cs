using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Services.AccessoryToBoat;
using AccessoryToBoat = WorldYachts.Data.Entities.AccessoryToBoat;

namespace WorldYachts.Model
{
    class AccessoryToBoatModel:IAccessoryToBoatModel
    {
        public event Func<object, Task> AccessoryToBoatModelChanged;
        
        private readonly IAccessoryToBoatService _accessoryToBoatService;

        public AccessoryToBoatModel(IAccessoryToBoatService accessoryToBoatService)
        {
            _accessoryToBoatService = accessoryToBoatService;
        }
        
        public async Task AddAsync(AccessoryToBoat item)
        {
            await _accessoryToBoatService.AddAsync(item);
            AccessoryToBoatModelChanged?.Invoke(item);
        }

        public async Task<IEnumerable<AccessoryToBoat>> GetAllAsync()
        {
            return await _accessoryToBoatService.GetAllAsync();
        }

        public async Task DeleteAsync(AccessoryToBoat item)
        {
            await _accessoryToBoatService.DeleteAsync(item.Id);
            AccessoryToBoatModelChanged?.Invoke(item);
        }

        public async Task UpdateAsync(AccessoryToBoat item)
        {
            await _accessoryToBoatService.UpdateAsync(item.Id, item);
            AccessoryToBoatModelChanged?.Invoke(item);
        }

        public async Task<AccessoryToBoat> GetByIdAsync(int id)
        {
            return await _accessoryToBoatService.GetByIdAsync(id);
        }

       

        

    }
}
