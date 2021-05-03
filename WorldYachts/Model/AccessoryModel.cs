using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Model
{
    public class AccessoryModel : IAccessoryModel
    {
        public event Func<object, Task> AccessoryModelChanged; 
            
        private readonly IAccessoryService _accessoryService;

        public AccessoryModel(IAccessoryService accessoryService)
        {
            _accessoryService = accessoryService;
        }
        
        public async Task AddAsync(Accessory item)
        {
            await _accessoryService.AddAsync(item);
            AccessoryModelChanged?.Invoke(item);
        }

        public async Task<IEnumerable<Accessory>> GetAllAsync()
        {
            return await _accessoryService.GetAllAsync();
        }

        public async Task DeleteAsync(Accessory accessory)
        {
            await _accessoryService.DeleteAsync(accessory.Id);
            AccessoryModelChanged?.Invoke(accessory);
        }

        public async Task UpdateAsync(Accessory item)
        {
            await _accessoryService.UpdateAsync(item.Id, item);
            AccessoryModelChanged?.Invoke(item);
        }

        public async Task<Accessory> GetByIdAsync(int id)
        {
            return await _accessoryService.GetByIdAsync(id);
        }
    }
}