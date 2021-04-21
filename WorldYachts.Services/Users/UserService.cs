using System.Threading.Tasks;
using WorldYachts.Services.Authenticate;

namespace WorldYachts.Services.Users
{
    public class UserService:IUserService
    {
        private readonly WebClientService _webClientService;
        private readonly AuthUser _authUser;

        public UserService(WebClientService webClientService, AuthUser authUser)
        {
            _webClientService = webClientService;
            _authUser = authUser;
        }
        
        public async Task AuthenticateAsync(string username, string password)
        {
            var requestData = new AuthenticateRequest(){Username = username, Password = password};
           
            var response = await _webClientService
                .PostAsync<AuthenticateRequest, AuthenticateResponse>("users/authenticate", requestData);
            
            if (response != null)
            {
                _authUser.Authenticate(response);
            }
            //var response = await webClientWorker
            //    .PostAsync<AuthenticateRequest,AuthenticateResponse>("users/authenticate", request);

            //var user = AuthUser.GetInstance(response);
        }
    }
}
