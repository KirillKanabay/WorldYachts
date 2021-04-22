using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldYachts.Services.Boat
{
    public interface IBoatService
    {
        Task<Data.Entities.Boat> GetByIdAsync(int id);
        Task<IEnumerable<Data.Entities.Boat>> GetAllAsync();
        Task<Data.Entities.Boat> AddAsync(Data.Entities.Boat boat);
        Task<Data.Entities.Boat> UpdateAsync(int id, Data.Entities.Boat boat);
        Task<Data.Entities.Boat> DeleteAsync(int id);
    }
}