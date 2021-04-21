using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Services.Admin;
using WorldYachts.Services.Authenticate;
using WorldYachts.Services.SalesPerson;

namespace WorldYachts.Services
{
    public class AuthUser
    {
        private static AuthUser _instance;

        private AuthUser()
        {
        }

        public static AuthUser GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AuthUser();
            }

            return _instance;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public IUser User { get; set; }

        public TypeOfUser TypeOfUser => Role switch
        {
            "Admin" => TypeOfUser.Admin,
            "Sales Person" => TypeOfUser.SalesPerson
        };

        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(Token);
        public async Task Authenticate(AuthenticateResponse authenticateResponse)
        {
            Id = authenticateResponse.Id;
            Username = authenticateResponse.Username;
            Email = authenticateResponse.Email;
            Role = authenticateResponse.Role;
            UserId = authenticateResponse.UserId;
            Token = authenticateResponse.Token;

            await SetUser();
        }

        private async Task SetUser()
        {
            User = Role switch
            {
                "Admin" => await new AdminService().GetByIdAsync(Id),
                "Sales Person" => await new SalesPersonService().GetByIdAsync(Id),
                _ => throw new ArgumentException(
                    "Доступ отклонен. Это приложение только для менеджеров и администраторов")
            };
        }
    }
}
