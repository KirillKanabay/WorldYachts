using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Services.WebApiServices
{
    public class OrderDetailWebService:IOrderDetailService
    {
        private readonly IWebClientService _webClient;
        private const string Path = "orders/details";

        public OrderDetailWebService(IWebClientService webClient)
        {
            _webClient = webClient;
        }
        public async Task<OrderDetail> GetByIdAsync(int id)
        {
            var response = await _webClient.GetAsync<OrderDetail>(Path, id);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException("Детали заказа с таким идентификатором не существует.");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllAsync()
        {
            var response = await _webClient.GetAsync<IEnumerable<OrderDetail>>(Path);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<OrderDetail> AddAsync(OrderDetail orderDetail)
        {
            var response = await _webClient.PostAsync<OrderDetail, OrderDetail>(Path, orderDetail);

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

        public async Task<OrderDetail> UpdateAsync(int id, OrderDetail orderDetail)
        {
            var response = await _webClient.PutAsync<OrderDetail, OrderDetail>(Path, id, orderDetail);

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

        public async Task<OrderDetail> DeleteAsync(int id)
        {
            var response = await _webClient.DeleteAsync<OrderDetail>(Path, id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }
    }
}
