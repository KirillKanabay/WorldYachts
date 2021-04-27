using System.Threading.Tasks;
using WorldYachts.Services.Authenticate;

namespace WorldYachts.Services.Users
{
    public interface IUserService
    {
        Task AuthenticateAsync(AuthenticateRequest request);
    }
}
