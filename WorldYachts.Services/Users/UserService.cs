using System.Threading.Tasks;
using WorldYachts.Services.Authenticate;

namespace WorldYachts.Services.Users
{
    public class UserService:IUserService
    {
        private readonly IWebClientService _webClient;
        private readonly AuthUser _authUser;

        public UserService(IWebClientService webClient, AuthUser authUser)
        {
            _webClient = webClient;
            _authUser = authUser;
        }
        
        public async Task AuthenticateAsync(AuthenticateRequest request)
        {
           var response = await _webClient
                .PostAsync<AuthenticateRequest, AuthenticateResponse>("users/authenticate", request);

            if (response != null)
            {
                await _authUser.Authenticate(response.Data);
                _webClient.Token = _authUser.Token;
            }
        }
    }
}
