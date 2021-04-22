using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WorldYachts.Services
{
    public class WebClientService : IWebClientService
    {
        private WebClientService()
        {
            //ConnectionUrl = configuration["ConnectionUrl"];

            _client = new HttpClient();

            _client.BaseAddress = new Uri("https://localhost:44311/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static WebClientService _instance;

        public static WebClientService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WebClientService();
            }

            return _instance;
        }

        private HttpClient _client { get; set; }
        private string _token;
        public string ConnectionUrl { get; set; }

        public string Token
        {
            get => _token;
            set
            {
                _token = value;
                _client.DefaultRequestHeaders.Add("Authorization", _token);
            }
        }

        public async Task<TResponse> GetAsync<TResponse>(string path, int? id = null)
            where TResponse : class
        {
            TResponse entity = null;
            HttpResponseMessage response = await _client.GetAsync($"api/{path}" + (id != null ? $"/{id}" : ""));
            if (response.IsSuccessStatusCode)
            {
                entity = await response.Content.ReadAsAsync<TResponse>();
            }

            return entity;
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string path, TRequest entity)
            where TRequest : class
            where TResponse : class
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"api/{path}", entity);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResponse>();
        }

        public async Task<TResponse> UpdateAsync<TRequest, TResponse>(TRequest entity, string path, int id)
            where TRequest : class
            where TResponse : class
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(
                $"api/{path}/{id}", entity);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResponse>();
        }

        public async Task<TResponse> DeleteAsync<TResponse>(string path, int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(
                $"api/{path}/{id}");

            return await response.Content.ReadAsAsync<TResponse>();
        }
    }
}