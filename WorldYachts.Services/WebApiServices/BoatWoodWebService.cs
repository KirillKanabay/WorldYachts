using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Services.WebApiServices
{
    public class BoatWoodWebService:IBoatWoodService
    {
        private readonly IWebClientService _webClient;
        private const string Path = "boats/woods";
        public BoatWoodWebService(IWebClientService webClient)
        {
            _webClient = webClient;
        }
        public async Task<Data.Entities.BoatWood> GetByIdAsync(int id)
        {
            var response = await _webClient.GetAsync<Data.Entities.BoatWood>(Path, id);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException("Типа дерева лодки с таким идентификатором не существует.");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<IEnumerable<Data.Entities.BoatWood>> GetAllAsync()
        {
            var response = await _webClient.GetAsync<IEnumerable<Data.Entities.BoatWood>>(Path);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<Data.Entities.BoatWood> AddAsync(Data.Entities.BoatWood boatWood)
        {
            var response = await _webClient.PostAsync<Data.Entities.BoatWood, Data.Entities.BoatWood>(Path, boatWood);

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

        public async Task<Data.Entities.BoatWood> UpdateAsync(int id, Data.Entities.BoatWood boatWood)
        {
            var response = await _webClient.PutAsync<Data.Entities.BoatWood, Data.Entities.BoatWood>(Path, id, boatWood);

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

        public async Task<Data.Entities.BoatWood> DeleteAsync(int id)
        {
            var response = await _webClient.DeleteAsync<Data.Entities.BoatWood>(Path, id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }
    }
}
