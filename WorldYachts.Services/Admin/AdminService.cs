using System;
using System.Collections.Generic;
using System.Net;
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
            var response = await _webClient.GetAsync<Data.Entities.Admin>("users", id);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException("Пользователь не найден");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }
    }
}
