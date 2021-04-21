using System.Threading.Tasks;
using WorldYachts.Services.Authenticate;

namespace WorldYachts.Services.Users
{
    public class UserService:IUserService
    {
        private readonly IWebClientService _webClientService;
        private readonly AuthUser _authUser;

        public UserService()
        {
            _webClientService = WebClientService.GetInstance();
            _authUser = AuthUser.GetInstance();
        }
        
        public async Task AuthenticateAsync(string username, string password)
        {
            var requestData = new AuthenticateRequest(){Username = username, Password = password};
           
            var response = await _webClientService
                .PostAsync<AuthenticateRequest, AuthenticateResponse>("users/authenticate", requestData);
            
            if (response != null)
            {
               await _authUser.Authenticate(response);
            }
            //var response = await webClientWorker
            //    .PostAsync<AuthenticateRequest,AuthenticateResponse>("users/authenticate", request);

            //var user = AuthUser.GetInstance(response);
        }
    }
}
