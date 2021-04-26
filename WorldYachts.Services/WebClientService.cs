using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WorldYachts.Services
{
    public class WebClientService : IWebClientService
    {
        private IConfiguration Configuration;
        private void GetConfigurations()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("WebClientSettings.json", optional: false, reloadOnChange: true);
            Configuration = configurationBuilder.Build();
        }

        private void ConfigureWebClient()
        {
            _client = new HttpClient();

            _client.BaseAddress = new Uri(Configuration.GetConnectionString("Default"));
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public WebClientService()
        {
            GetConfigurations();
            ConfigureWebClient();
        }

        private static WebClientService _instance;

        public static WebClientService GetInstance(IConfiguration configuration = null)
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

        public async Task<Response<TResponse>> GetAsync<TResponse>(string path, int? id = null)
            where TResponse : class
        {
            HttpResponseMessage response = await _client.GetAsync($"api/{path}" + (id != null ? $"/{id}" : ""));

            return new Response<TResponse>()
            {
                Data = await response.Content.ReadAsAsync<TResponse>(),
                StatusCode = response.StatusCode
            };
        }

        public async Task<Response<TResponse>> PostAsync<TRequest, TResponse>(string path, TRequest entity)
            where TRequest : class
            where TResponse : class
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"api/{path}", entity);

            return new Response<TResponse>()
            {
                Data = await response.Content.ReadAsAsync<TResponse>(),
                StatusCode = response.StatusCode,
            };
        }

        public async Task<Response<TResponse>> PutAsync<TRequest, TResponse>(string path, int id, TRequest entity) 
            where TRequest : class where TResponse : class
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(
                $"api/{path}/{id}", entity);

            return new Response<TResponse>()
            {
                Data = await response.Content.ReadAsAsync<TResponse>(),
                StatusCode = response.StatusCode
            };
        }

        public async Task<Response<TResponse>> DeleteAsync<TResponse>(string path, int id)
            where TResponse : class
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/{path}/{id}");

            return new Response<TResponse>
            {
                Data = await response.Content.ReadAsAsync<TResponse>(),
                StatusCode = response.StatusCode
            };
        }
    }
}