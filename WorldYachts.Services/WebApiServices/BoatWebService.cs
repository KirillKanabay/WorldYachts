using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using WorldYachts.Data.ViewModels;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Services.WebApiServices
{
    public class BoatWebService : IBoatService
    {
        private readonly IWebClientService _webClient;
        private readonly IMapper _mapper;

        private const string Path = "boats"; 
        public BoatWebService(IWebClientService webClient, IMapper mapper)
        {
            _webClient = webClient;
            _mapper = mapper;
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
            var boatModel = _mapper.Map<BoatModel>(boat);
            var response = await _webClient.PostAsync<BoatModel, Data.Entities.Boat>(Path, boatModel);

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
            var boatModel = _mapper.Map<BoatModel>(boat);
            var response = await _webClient.PutAsync<BoatModel, Data.Entities.Boat>(Path, id, boatModel);
            
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
