using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.Data.ViewModels;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Services.WebApiServices
{
    public class CustomerWebService:ICustomerService
    {
        private readonly IWebClientService _webClient;
        private const string Path = "customers";

        public CustomerWebService(IWebClientService webClient)
        {
            _webClient = webClient;
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            var response = await _webClient.GetAsync<Customer>(Path, id);
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

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var response = await _webClient.GetAsync<IEnumerable<Customer>>(Path);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<Customer> AddAsync(CustomerUserViewModel customerUserViewModel)
        {
            var response = await _webClient.PostAsync<CustomerUserViewModel, Customer>(Path, customerUserViewModel);

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

        public async Task<Customer> UpdateAsync(int id, Customer customer)
        {
            var response = await _webClient.PutAsync<Customer, Customer>(Path, id, customer);

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

        public async Task<Customer> DeleteAsync(int id)
        {
            var response = await _webClient.DeleteAsync<Customer>(Path, id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }
    }
}
