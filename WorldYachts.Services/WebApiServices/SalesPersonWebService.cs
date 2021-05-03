using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.Data.ViewModels;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Services.WebApiServices
{
    public class SalesPersonWebService:ISalesPersonService
    {
        private readonly IWebClientService _webClient;
        private const string Path = "salespersons";
        public SalesPersonWebService(IWebClientService webClientService)
        {
            _webClient = webClientService;
        }
        public async Task<SalesPerson> GetByIdAsync(int id)
        {
            var response = await _webClient.GetAsync<SalesPerson>(Path, id);
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

        public async Task<IEnumerable<SalesPerson>> GetAllAsync()
        {
            var response = await _webClient.GetAsync<IEnumerable<Data.Entities.SalesPerson>>(Path);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<SalesPerson> AddAsync(SalesPersonUserViewModel salesPersonModel)
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

        public async Task<SalesPerson> UpdateAsync(int id, SalesPerson salesPersonModel)
        {
            var response = await _webClient.PutAsync<SalesPerson, SalesPerson>(Path, id, salesPersonModel);

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

        public async Task<SalesPerson> DeleteAsync(int id)
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
