using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Services.Partner;

namespace WorldYachts.Model
{
    public class PartnerModel : IPartnerModel
    {
        private readonly IPartnerService _partnerService;
        
        public event Func<object, Task> PartnerModelChanged;
        
        public PartnerModel(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }
       
        /// <summary>
        /// Асинхронный метод добавления партнера в БД
        /// </summary>
        /// <param name="partner">Добавляемый партнер</param>
        /// <returns></returns>
        public async Task AddAsync(Partner partner)
        {
            await _partnerService.AddAsync(partner);
            PartnerModelChanged?.Invoke(partner);
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
        public async Task DeleteAsync(Partner partner)
        {
            await _partnerService.DeleteAsync(partner.Id);
            PartnerModelChanged?.Invoke(partner);
        }
        
        /// <summary>
        /// Асинхронный метод изменения партнера 
        /// </summary>
        /// <param name="partner">Партнер</param>
        /// <returns></returns>
        public async Task UpdateAsync(Partner partner)
        {
            await _partnerService.UpdateAsync(partner.Id, partner);
            PartnerModelChanged?.Invoke(partner);
        }
        
        public async Task<Partner> GetByIdAsync(int id)
        {
            return await _partnerService.GetByIdAsync(id);
        }
    }
}