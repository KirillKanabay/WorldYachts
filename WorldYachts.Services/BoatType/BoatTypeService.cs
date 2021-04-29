using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WorldYachts.Services.BoatType
{
    public class BoatTypeService:IBoatTypeService
    {
        private readonly IWebClientService _webClient;
        private const string Path = "boats/types";
        public BoatTypeService(IWebClientService webClient)
        {
            _webClient = webClient;
        }
        public async Task<Data.Entities.BoatType> GetByIdAsync(int id)
        {
            var response = await _webClient.GetAsync<Data.Entities.BoatType>(Path, id);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException("Типа лодки с таким идентификатором не существует.");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<IEnumerable<Data.Entities.BoatType>> GetAllAsync()
        {
            var response = await _webClient.GetAsync<IEnumerable<Data.Entities.BoatType>>(Path);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<Data.Entities.BoatType> AddAsync(Data.Entities.BoatType boatType)
        {
            var response = await _webClient.PostAsync<Data.Entities.BoatType, Data.Entities.BoatType>(Path, boatType);

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

        public async Task<Data.Entities.BoatType> UpdateAsync(int id, Data.Entities.BoatType boatType)
        {
            var response = await _webClient.PutAsync<Data.Entities.BoatType, Data.Entities.BoatType>(Path, id, boatType);

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

        public async Task<Data.Entities.BoatType> DeleteAsync(int id)
        {
            var response = await _webClient.DeleteAsync<Data.Entities.BoatType>(Path, id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }
    }
}
