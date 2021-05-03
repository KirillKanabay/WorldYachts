using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WorldYachts.Services.Models;
using WorldYachts.Services.ViewModels;

namespace WorldYachts.Services.SalesPerson
{
    public class SalesPersonService:ISalesPersonService
    {
        private readonly IWebClientService _webClient;
        private const string Path = "salespersons";
        public SalesPersonService(IWebClientService webClientService)
        {
            _webClient = webClientService;
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

        public async Task<IEnumerable<Data.Entities.SalesPerson>> GetAllAsync()
        {
            var response = await _webClient.GetAsync<IEnumerable<Data.Entities.SalesPerson>>(Path);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<Data.Entities.SalesPerson> AddAsync(SalesPersonUserViewModel salesPersonModel)
        {
            var response = await _webClient.PostAsync<SalesPersonUserViewModel, Data.Entities.SalesPerson>(Path, salesPersonModel);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception($"Проверьте правильность заполненных данных!");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<Data.Entities.SalesPerson> UpdateAsync(int id, SalesPersonUserViewModel salesPersonModel)
        {
            var response = await _webClient.PutAsync<SalesPersonUserViewModel, Data.Entities.SalesPerson>(Path, id, salesPersonModel);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception($"Проверьте правильность заполненных данных!");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<Data.Entities.SalesPerson> DeleteAsync(int id)
        {
            var response = await _webClient.DeleteAsync<Data.Entities.SalesPerson>(Path, id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }
    }
}
