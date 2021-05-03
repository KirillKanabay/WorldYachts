using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Services.WebApiServices
{
    public class PartnerWebService:IPartnerService
    {
        private readonly IWebClientService _webClient;
        private const string Path = "partners";

        public PartnerWebService(IWebClientService webClient)
        {
            _webClient = webClient;
        }
        public async Task<Data.Entities.Partner> GetByIdAsync(int id)
        {
            var response = await _webClient.GetAsync<Data.Entities.Partner>(Path, id);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException("Партнера с таким идентификатором не существует.");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<IEnumerable<Data.Entities.Partner>> GetAllAsync()
        {
            var response = await _webClient.GetAsync<IEnumerable<Data.Entities.Partner>>(Path);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<Data.Entities.Partner> AddAsync(Data.Entities.Partner partner)
        {
            var response = await _webClient.PostAsync<Data.Entities.Partner, Data.Entities.Partner>(Path, partner);

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

        public async Task<Data.Entities.Partner> UpdateAsync(int id, Data.Entities.Partner partner)
        {
            var response = await _webClient.PutAsync<Data.Entities.Partner, Data.Entities.Partner>(Path, id, partner);

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

        public async Task<Data.Entities.Partner> DeleteAsync(int id)
        {
            var response = await _webClient.DeleteAsync<Data.Entities.Partner>(Path, id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }
    }
}
