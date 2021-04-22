using System.Net;
using System.Threading.Tasks;

namespace WorldYachts.Services
{
    public interface IWebClientService
    {
        /// <summary>
        /// JWT - токен
        /// </summary>
        string Token { get; set; }

        /// <summary>
        /// GET - запрос
        /// </summary>
        /// <typeparam name="TResponse">Тип получаемых данных</typeparam>
        /// <param name="path">Путь</param>
        /// <param name="id">id - записи</param>
        /// <returns></returns>
        Task<TResponse> GetAsync<TResponse>(string path, int? id = null)
            where TResponse : class;

        /// <summary>
        /// POST - запрос
        /// </summary>
        /// <typeparam name="TRequest">Тип отправляемых данных</typeparam>
        /// <typeparam name="TResponse">Тип получаемых данных</typeparam>
        /// <param name="path">Путь</param>
        /// <param name="entity">Отправляемый объект</param>
        /// <returns></returns>
        Task<TResponse> PostAsync<TRequest, TResponse>(string path, TRequest entity)
            where TRequest : class
            where TResponse : class;

        /// <summary>
        /// PUT - запрос
        /// </summary>
        /// <typeparam name="TRequest">Тип отправляемых данных</typeparam>
        /// <typeparam name="TResponse">Тип получаемых данных</typeparam>
        /// <param name="entity">Отправляемый объект</param>
        /// <param name="path">Путь</param>
        /// <param name="id">id - изменяемого объекта</param>
        /// <returns></returns>
        Task<TResponse> PutAsync<TRequest, TResponse>(string path, int id, TRequest entity)
            where TRequest : class
            where TResponse : class;

        /// <summary>
        /// DELETE - запрос
        /// </summary>
        /// <param name="path">Путь</param>
        /// <param name="id">id - удаляемой записи</param>
        /// <returns></returns>
        Task<TResponse> DeleteAsync<TResponse>(string path, int id);
    }
}