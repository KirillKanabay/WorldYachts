using System.Net;

namespace WorldYachts.Services
{
    public class Response<TEntity> where TEntity : class
    {
        public HttpStatusCode StatusCode { get; set; }
        public TEntity Data { get; set; }
    }
}
