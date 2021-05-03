using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Model
{
    class BoatModel : IBoatModel
    {
        public event Func<object, Task> BoatModelChanged;
        private readonly IBoatService _boatService;
        public BoatModel(IBoatService boatService)
        {
            _boatService = boatService;
        }

        /// <summary>
        /// Асинхронный метод добавления лодки в БД
        /// </summary>
        /// <returns></returns>
        public async Task AddAsync(Boat boat)
        {
            await _boatService.AddAsync(boat);
            BoatModelChanged?.Invoke(boat);
        }
        /// <summary>
        /// Асинхронный метод загрузки лодок из БД
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Boat>> GetAllAsync()
        {
            return await _boatService.GetAllAsync();
        }
        
        /// <summary>
        /// Асинхронный метод удаления коллекции лодок из БД
        /// </summary>
        /// <param name="boat">Удаляемая лодка</param>
        /// <returns></returns>
        public async Task DeleteAsync(Boat boat)
        {
            await _boatService.DeleteAsync(boat.Id);
            BoatModelChanged?.Invoke(boat);
        }

        /// <summary>
        /// Сохранение измененной лодки в БД
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAsync(Boat boat)
        {
            await _boatService.UpdateAsync(boat.Id, boat);
            BoatModelChanged?.Invoke(boat);
        }
    
        public async Task<Boat> GetByIdAsync(int id)
        {
            return await _boatService.GetByIdAsync(id);
        }
    }
}
