using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldYachts.Services.Partner
{
    public interface IPartnerService
    {
        Task<Data.Entities.Partner> GetByIdAsync(int id);
        Task<IEnumerable<Data.Entities.Partner>> GetAllAsync();
        Task<Data.Entities.Partner> AddAsync(Data.Entities.Partner partner);
        Task<Data.Entities.Partner> UpdateAsync(int id, Data.Entities.Partner partner);
        Task<Data.Entities.Partner> DeleteAsync(int id);
    }
}
