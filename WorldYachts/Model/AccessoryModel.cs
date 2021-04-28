using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Services.Accessory;

namespace WorldYachts.Model
{
    public class AccessoryModel : IAccessoryModel
    {
        private readonly IAccessoryService _accessoryService;

        public AccessoryModel(IAccessoryService accessoryService)
        {
            _accessoryService = accessoryService;
        }

        public Accessory LastAddedItem { get; set; }

        public async Task AddAsync(Accessory item)
        {
            await _accessoryService.AddAsync(item);
        }

        public async Task<IEnumerable<Accessory>> GetAllAsync()
        {
            return await _accessoryService.GetAllAsync();
        }

        public async Task DeleteAsync(IEnumerable<Accessory> removeItems)
        {
            foreach (var removeItem in removeItems)
            {
                await _accessoryService.DeleteAsync(removeItem.Id);
            }
        }

        public async Task UpdateAsync(Accessory item)
        {
            await _accessoryService.UpdateAsync(item.Id, item);
        }

        public async Task<Accessory> GetByIdAsync(int id)
        {
            return await _accessoryService.GetByIdAsync(id);
        }
    }
}