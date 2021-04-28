using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.DependencyInjections.Models
{
    public interface IPartnerModel
    {
        event Func<object, Task> PartnerModelChanged;

        /// <summary>
        /// Асинхронный метод добавления партнера в БД
        /// </summary>
        /// <param name="partner">Добавляемый партнер</param>
        /// <returns></returns>
        Task AddAsync(Partner partner);

        /// <summary>
        /// Асинхронный метод загрузки партнеров из БД
        /// </summary>
        /// <returns>Коллекция партнеров</returns>
        Task<IEnumerable<Partner>> GetAllAsync();

        /// <summary>
        /// Асинхронный метод удаления коллекции партнеров из БД
        /// </summary>
        /// <param name="removePartners">Коллекция партнеров</param>
        /// <returns></returns>
        Task DeleteAsync(Partner partner);

        /// <summary>
        /// Асинхронный метод изменения партнера 
        /// </summary>
        /// <param name="partner">Партнер</param>
        /// <returns></returns>
        Task UpdateAsync(Partner partner);

        Task<Partner> GetByIdAsync(int id);
    }
}