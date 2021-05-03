using System;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Services.Admin;
using WorldYachts.Services.Authenticate;
using WorldYachts.Services.SalesPerson;

namespace WorldYachts.Services
{
    public class AuthUser
    {
        private readonly IAdminService _adminService;
        private readonly ISalesPersonService _salesPersonService;

        public AuthUser(IAdminService adminService, ISalesPersonService salesPersonService)
        {
            _adminService = adminService;
            _salesPersonService = salesPersonService;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public IUser User { get; set; }

        public TypeOfUser TypeOfUser { get; set; }

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
                "Admin" => await _adminService.GetByIdAsync(Id),
                "Sales Person" => await _salesPersonService.GetByIdAsync(Id),
                _ => throw new ArgumentException(
                    "Доступ отклонен. Это приложение только для менеджеров и администраторов")
            };
            TypeOfUser = Role switch
            {
                "Admin" => TypeOfUser.Admin,
                "Sales Person" => TypeOfUser.SalesPerson,
                _ => TypeOfUser.Unauthorized,
            };
        }
    }
}
