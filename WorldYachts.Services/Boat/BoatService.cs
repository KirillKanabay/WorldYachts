using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WorldYachts.Services.Boat
{
    public class BoatService : IBoatService
    {
        private readonly IWebClientService _webClient;
        private const string Path = "boats"; 
        public BoatService()
        {
            _webClient = WebClientService.GetInstance();
        }

        public async Task<Data.Entities.Boat> GetByIdAsync(int id)
        {
            return await _webClient.GetAsync<Data.Entities.Boat>(Path, id);
        }

        public async Task<IEnumerable<Data.Entities.Boat>> GetAllAsync()
        {
            return await _webClient.GetAsync<IEnumerable<Data.Entities.Boat>>(Path);
        }

        public async Task<Data.Entities.Boat> AddAsync(Data.Entities.Boat boat)
        {
            return await _webClient.PostAsync<Data.Entities.Boat, Data.Entities.Boat>(Path, boat);
        }

        public async Task<Data.Entities.Boat> UpdateAsync(int id, Data.Entities.Boat boat)
        {
            return await _webClient.PutAsync<Data.Entities.Boat, Data.Entities.Boat>(Path, id, boat);
        }

        public async Task<Data.Entities.Boat> DeleteAsync(int id)
        {
            return await _webClient.DeleteAsync<Data.Entities.Boat>(Path, id);
        }
    }
}
