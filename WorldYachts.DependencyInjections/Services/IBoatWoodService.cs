using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldYachts.DependencyInjections.Services
{
    public interface IBoatWoodService
    {
        Task<Data.Entities.BoatWood> GetByIdAsync(int id);
        Task<IEnumerable<Data.Entities.BoatWood>> GetAllAsync();
        Task<Data.Entities.BoatWood> AddAsync(Data.Entities.BoatWood boatWood);
        Task<Data.Entities.BoatWood> UpdateAsync(int id, Data.Entities.BoatWood boatWood);
        Task<Data.Entities.BoatWood> DeleteAsync(int id);
    }
}
