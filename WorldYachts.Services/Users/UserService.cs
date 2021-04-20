using System.Threading.Tasks;
using WorldYachts.Services.Authenticate;

namespace WorldYachts.Services.Users
{
    public class UserService
    {
        public async Task LoginAsync(string username, string password)
        {
            //var request = new AuthenticateRequest(){Username = username, Password = password};
            ////var webClientWorker = WebClientService.GetInstance("https://localhost:5001/");
            
            //var response = await webClientWorker
            //    .PostAsync<AuthenticateRequest,AuthenticateResponse>("users/authenticate", request);
            
            //var user = AuthUser.GetInstance(response);
        }
    }
}
