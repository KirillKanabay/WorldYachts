using System.Net;
using System.Threading.Tasks;

namespace WorldYachts.Services
{
    public interface IWebClientService
    {
        Task<TResponse> GetAsync<TResponse>(string path, int? id = null)
            where TResponse : class;

        Task<TResponse> PostAsync<TRequest, TResponse>(string path, TRequest entity)
            where TRequest : class
            where TResponse : class;

        Task<TResponse> UpdateProductAsync<TRequest, TResponse>(TRequest entity, string path, int id)
            where TRequest : class
            where TResponse : class;

        Task<HttpStatusCode> DeleteProductAsync(string path, int id);
    }
}