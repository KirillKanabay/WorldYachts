using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WorldYachts.Services.Accessory
{
    public class AccessoryService:IAccessoryService
    {
        private readonly IWebClientService _webClient;
        private const string Path = "accessories";

        public AccessoryService(IWebClientService webClient)
        {
            _webClient = webClient;
        }
        public async Task<Data.Entities.Accessory> GetByIdAsync(int id)
        {
            var response = await _webClient.GetAsync<Data.Entities.Accessory>(Path, id);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException("Аксессуара с таким идентификатором не существует.");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<IEnumerable<Data.Entities.Accessory>> GetAllAsync()
        {
            var response = await _webClient.GetAsync<IEnumerable<Data.Entities.Accessory>>(Path);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<Data.Entities.Accessory> AddAsync(Data.Entities.Accessory accessory)
        {
            var response = await _webClient.PostAsync<Data.Entities.Accessory, Data.Entities.Accessory>(Path, accessory);

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

        public async Task<Data.Entities.Accessory> UpdateAsync(int id, Data.Entities.Accessory accessory)
        {
            var response = await _webClient.PutAsync<Data.Entities.Accessory, Data.Entities.Accessory>(Path, id, accessory);

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

        public async Task<Data.Entities.Accessory> DeleteAsync(int id)
        {
            var response = await _webClient.DeleteAsync<Data.Entities.Accessory>(Path, id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }
    }
}
