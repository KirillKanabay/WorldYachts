using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.DependencyInjections.Models
{
    public interface IAccessoryModel
    {
        Task AddAsync(Accessory item);
        Task<IEnumerable<Accessory>> GetAllAsync();
        Task DeleteAsync(IEnumerable<Accessory> removeItems);
        Task UpdateAsync(Accessory item);
        Task<Accessory> GetByIdAsync(int id);
    }
}