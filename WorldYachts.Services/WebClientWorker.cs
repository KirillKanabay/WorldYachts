using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WorldYachts.Services
{
    class WebClientWorker
    {
        #region Singleton
        private WebClientWorker(string connectionUrl)
        {
            ConnectionUrl = connectionUrl;
            
            _client = new HttpClient();

            _client.BaseAddress = new Uri(ConnectionUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static WebClientWorker _instance;

        private static readonly object _lock = new object();

        public static WebClientWorker GetInstance(string connectionUrl)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new WebClientWorker(connectionUrl);
                    }
                }
            }

            return _instance;
        }

        public static WebClientWorker GetInstance()
        {
            return _instance;
        }
        #endregion

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
            where TResponse: class
        {
            TResponse entity = null;
            HttpResponseMessage response = await _client.GetAsync($"api/{path}/{id ?? ' '}");
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

        public async Task<TResponse> UpdateProductAsync<TRequest, TResponse>(TRequest entity, string path, int id)
            where TRequest : class
            where TResponse : class
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(
                $"api/{path}/{id}", entity);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResponse>();
        }

        public async Task<HttpStatusCode> DeleteProductAsync(string path, int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(
                $"api/{path}/{id}");
            return response.StatusCode;
        }
    }
}
