using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Services.WebApiServices
{
    public class InvoiceWebService:IInvoiceService
    {
        private readonly IWebClientService _webClient;
        private const string Path = "invoices";

        public InvoiceWebService(IWebClientService webClient)
        {
            _webClient = webClient;
        }
        public async Task<Invoice> GetByIdAsync(int id)
        {
            var response = await _webClient.GetAsync<Invoice>(Path, id);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException("Счета с таким идентификатором не существует.");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            var response = await _webClient.GetAsync<IEnumerable<Invoice>>(Path);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            var response = await _webClient.PostAsync<Invoice, Invoice>(Path, invoice);

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

        public async Task<Invoice> UpdateAsync(int id, Invoice invoice)
        {
            var response = await _webClient.PutAsync<Invoice, Invoice>(Path, id, invoice);

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

        public async Task<Invoice> DeleteAsync(int id)
        {
            var response = await _webClient.DeleteAsync<Invoice>(Path, id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }
    }
}
