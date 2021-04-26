using System.Threading.Tasks;
using WorldYachts.Services.Authenticate;

namespace WorldYachts.Services.Users
{
    public class UserService:IUserService
    {
        private readonly IWebClientService _webClient;
        private readonly AuthUser _authUser;

        public UserService(IWebClientService webClient)
        {
            _webClient = webClient;
            _authUser = AuthUser.GetInstance();
        }
        
        public async Task AuthenticateAsync(string username, string password)
        {
            var requestData = new AuthenticateRequest(){Username = username, Password = password};
           
            var response = await _webClient
                .PostAsync<AuthenticateRequest, AuthenticateResponse>("users/authenticate", requestData);

            if (response != null)
            {
                await _authUser.Authenticate(response.Data);
                _webClient.Token = _authUser.Token;
            }
        }
    }
}
