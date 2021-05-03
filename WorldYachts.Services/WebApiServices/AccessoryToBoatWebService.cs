using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Services.WebApiServices
{
    public class AccessoryToBoatWebService: IAccessoryToBoatService
    {
        private readonly IWebClientService _webClient;
        private const string Path = "accessories/fits";
        public AccessoryToBoatWebService(IWebClientService webClient)
        {
            _webClient = webClient;
        }
        public async Task<Data.Entities.AccessoryToBoat> GetByIdAsync(int id)
        {
            var response = await _webClient.GetAsync<Data.Entities.AccessoryToBoat>(Path, id);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException("Совместимости с таким идентификатором не существует.");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<IEnumerable<Data.Entities.AccessoryToBoat>> GetAllAsync()
        {
            var response = await _webClient.GetAsync<IEnumerable<Data.Entities.AccessoryToBoat>>(Path);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<Data.Entities.AccessoryToBoat> AddAsync(Data.Entities.AccessoryToBoat accessoryToBoat)
        {
            var response = await _webClient
                .PostAsync<Data.Entities.AccessoryToBoat, Data.Entities.AccessoryToBoat>(Path, accessoryToBoat);

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

        public async Task<Data.Entities.AccessoryToBoat> UpdateAsync(int id, Data.Entities.AccessoryToBoat accessoryToBoat)
        {
            var response = await _webClient
                .PutAsync<Data.Entities.AccessoryToBoat, Data.Entities.AccessoryToBoat>(Path, id, accessoryToBoat);

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

        public async Task<Data.Entities.AccessoryToBoat> DeleteAsync(int id)
        {
            var response = await _webClient.DeleteAsync<Data.Entities.AccessoryToBoat>(Path, id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }
    }
}
