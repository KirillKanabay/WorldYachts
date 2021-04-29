using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldYachts.Services.BoatType
{
    public interface IBoatTypeService
    {
        Task<Data.Entities.BoatType> GetByIdAsync(int id);
        Task<IEnumerable<Data.Entities.BoatType>> GetAllAsync();
        Task<Data.Entities.BoatType> AddAsync(Data.Entities.BoatType boatType);
        Task<Data.Entities.BoatType> UpdateAsync(int id, Data.Entities.BoatType boatType);
        Task<Data.Entities.BoatType> DeleteAsync(int id);
    }
}
