using System;
using System.Net;
using System.Threading.Tasks;

namespace WorldYachts.Services.SalesPerson
{
    public class SalesPersonService:ISalesPersonService
    {
        private readonly IWebClientService _webClient;
        public SalesPersonService()
        {
            _webClient = WebClientService.GetInstance();
        }
        public async Task<Data.Entities.SalesPerson> GetByIdAsync(int id)
        {
            var response = await _webClient.GetAsync<Data.Entities.SalesPerson>("users", id);
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
