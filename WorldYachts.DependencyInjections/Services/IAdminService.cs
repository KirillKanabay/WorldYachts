using System.Threading.Tasks;

namespace WorldYachts.DependencyInjections.Services
{
    public interface IAdminService
    {
        Task<Data.Entities.Admin> GetByIdAsync(int id);
    }
}
