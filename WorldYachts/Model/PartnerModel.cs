using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.Services.Partner;

namespace WorldYachts.Model
{
    public class PartnerModel : IDataModel<Partner>
    {
        private readonly IPartnerService _partnerService;
        public PartnerModel(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }
        public Partner LastAddedItem { get; set; }

        /// <summary>
        /// Асинхронный метод добавления партнера в БД
        /// </summary>
        /// <param name="partner">Добавляемый партнер</param>
        /// <returns></returns>
        public async Task AddAsync(Partner partner)
        {
            await _partnerService.AddAsync(partner);
        }
        /// <summary>
        /// Асинхронный метод загрузки партнеров из БД
        /// </summary>
        /// <returns>Коллекция партнеров</returns>
        public async Task<IEnumerable<Partner>> GetAllAsync()
        {
            return await _partnerService.GetAllAsync();
        }
        
        /// <summary>
        /// Асинхронный метод удаления коллекции партнеров из БД
        /// </summary>
        /// <param name="removePartners">Коллекция партнеров</param>
        /// <returns></returns>
        public async Task DeleteAsync(IEnumerable<Partner> removePartners)
        {
            foreach (var removePartner in removePartners)
            {
                await _partnerService.DeleteAsync(removePartner.Id);
            }
        }
        /// <summary>
        /// Асинхронный метод изменения партнера 
        /// </summary>
        /// <param name="partner">Партнер</param>
        /// <returns></returns>
        public async Task UpdateAsync(Partner partner)
        {
            await _partnerService.UpdateAsync(partner.Id, partner);
        }
        
        public async Task<Partner> GetByIdAsync(int id)
        {
            return await _partnerService.GetByIdAsync(id);
        }
    }
}