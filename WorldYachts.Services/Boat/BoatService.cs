using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WorldYachts.Services.Boat
{
    public class BoatService : IBoatService
    {
        private readonly IWebClientService _webClient;
        private const string Path = "boats"; 
        public BoatService()
        {
            _webClient = WebClientService.GetInstance();
        }

        public async Task<Data.Entities.Boat> GetByIdAsync(int id)
        {
            var response = await _webClient.GetAsync<Data.Entities.Boat>(Path, id);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException("Лодки с таким идентификатором не существует.");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<IEnumerable<Data.Entities.Boat>> GetAllAsync()
        {
            var response = await _webClient.GetAsync<IEnumerable<Data.Entities.Boat>>(Path);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<Data.Entities.Boat> AddAsync(Data.Entities.Boat boat)
        {
            var response = await _webClient.PostAsync<Data.Entities.Boat, Data.Entities.Boat>(Path, boat);

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

        public async Task<Data.Entities.Boat> UpdateAsync(int id, Data.Entities.Boat boat)
        {
            var response = await _webClient.PutAsync<Data.Entities.Boat, Data.Entities.Boat>(Path, id, boat);
            
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

        public async Task<Data.Entities.Boat> DeleteAsync(int id)
        {
            var response = await _webClient.DeleteAsync<Data.Entities.Boat>(Path, id);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }
    }
}
