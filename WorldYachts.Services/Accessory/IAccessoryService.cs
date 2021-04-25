using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldYachts.Services.Accessory
{
    public interface IAccessoryService
    {
        Task<Data.Entities.Accessory> GetByIdAsync(int id);
        Task<IEnumerable<Data.Entities.Accessory>> GetAllAsync();
        Task<Data.Entities.Accessory> AddAsync(Data.Entities.Accessory accessory);
        Task<Data.Entities.Accessory> UpdateAsync(int id, Data.Entities.Accessory accessory);
        Task<Data.Entities.Accessory> DeleteAsync(int id);
    }
}
