using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorldYachts.Services.SalesPerson
{
    public class SalesPersonService:ISalesPersonService
    {
        private readonly IWebClientService _webClient;
        public SalesPersonService()
        {
            _webClient = WebClientService.GetInstance();
        }
        public async Task<Data.Entities.SalesPerson> GetByIdAsync(int id)
        {
            return await _webClient.GetAsync<Data.Entities.SalesPerson>("users", id);
        }
    }
}
