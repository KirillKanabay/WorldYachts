using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Services.Boat;

namespace WorldYachts.Model
{
    class BoatModel : IBoatModel
    {
        public event Func<object, Task> BoatModelChanged;
        private readonly IBoatService _service;
        public BoatModel()
        {
            _service = new BoatService();
        }

        /// <summary>
        /// Асинхронный метод добавления лодки в БД
        /// </summary>
        /// <returns></returns>
        public async Task AddAsync(Boat boat)
        {
            await _service.AddAsync(boat);
            BoatModelChanged?.Invoke(boat);
        }
        /// <summary>
        /// Асинхронный метод загрузки лодок из БД
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Boat>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }
        
        /// <summary>
        /// Асинхронный метод удаления коллекции лодок из БД
        /// </summary>
        /// <param name="removeBoats">Коллекция лодок</param>
        /// <returns></returns>
        public async Task DeleteAsync(Boat boat)
        {
            await _service.DeleteAsync(boat.Id);
            BoatModelChanged?.Invoke(boat);
        }

        /// <summary>
        /// Сохранение измененной лодки в БД
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAsync(Boat boat)
        {
            await _service.UpdateAsync(boat.Id, boat);
            BoatModelChanged?.Invoke(boat);
        }
    
        public async Task<Boat> GetByIdAsync(int id)
        {
            return await _service.GetByIdAsync(id);
        }
    }
}
