using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Services.WebApiServices
{
    public class ContractWebService:IContractService
    {
        private readonly IWebClientService _webClient;
        private const string Path = "contracts";

        public ContractWebService(IWebClientService webClient)
        {
            _webClient = webClient;
        }
        public async Task<Contract> GetByIdAsync(int id)
        {
            var response = await _webClient.GetAsync<Contract>(Path, id);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException("Контракта с таким идентификатором не существует.");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<IEnumerable<Contract>> GetAllAsync()
        {
            var response = await _webClient.GetAsync<IEnumerable<Contract>>(Path);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<Contract> AddAsync(Contract contract)
        {
            var response = await _webClient.PostAsync<Contract, Contract>(Path, contract);

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

        public async Task<Contract> UpdateAsync(int id, Contract contract)
        {
            var response = await _webClient.PutAsync<Contract, Contract>(Path, id, contract);

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

        public async Task<Contract> DeleteAsync(int id)
        {
            var response = await _webClient.DeleteAsync<Contract>(Path, id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }
    }
}
