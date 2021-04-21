using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorldYachts.Services.Admin
{
    public class AdminService:IAdminService
    {
        private readonly IWebClientService _webClient;
        public AdminService()
        {
            _webClient = WebClientService.GetInstance();
        }
        public async Task<Data.Entities.Admin> GetByIdAsync(int id)
        {
            return await _webClient.GetAsync<Data.Entities.Admin>("users", id);

        }
    }
}
