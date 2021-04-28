using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.DependencyInjections.Models
{
    public interface IAccessoryModel
    {
        event Func<object, Task> AccessoryModelChanged;
        Task AddAsync(Accessory item);
        Task<IEnumerable<Accessory>> GetAllAsync();
        Task DeleteAsync(Accessory accessory);
        Task UpdateAsync(Accessory item);
        Task<Accessory> GetByIdAsync(int id);
    }
}