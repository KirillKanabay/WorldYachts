using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldYachts.Services
{
    class EntityService<TEntity>:IService<TEntity>
    {
        private readonly IWebClientService _webClient;
        public EntityService()
        {
            _webClient = WebClientService.GetInstance();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = _webClient.GetAsync<TEntity>()
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(int id, TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
