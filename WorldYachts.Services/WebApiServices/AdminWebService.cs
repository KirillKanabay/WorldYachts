using System;
using System.Net;
using System.Threading.Tasks;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Services.WebApiServices
{
    public class AdminWebService:IAdminService
    {
        private readonly IWebClientService _webClient;
        public AdminWebService()
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
