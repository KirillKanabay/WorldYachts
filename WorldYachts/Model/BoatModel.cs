using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.Services.Boat;

namespace WorldYachts.Model
{
    class BoatModel:IDataModel<Boat>
    {
        private readonly IBoatService _service;

        public BoatModel()
        {
            _service = new BoatService();
        }
        public Boat LastAddedItem { get; set; }

        /// <summary>
        /// Асинхронный метод добавления лодки в БД
        /// </summary>
        /// <returns></returns>
        public async Task AddAsync(Boat boat)
        {
            await _service.AddAsync(boat);
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
        public async Task DeleteAsync(IEnumerable<Boat> removeBoats)
        {
            foreach (var removeBoat in removeBoats)
            {
                await _service.DeleteAsync(removeBoat.Id);
            }
        }

        
        /// <summary>
        /// Сохранение измененной лодки в БД
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAsync(Boat boat)
        {
            await _service.UpdateAsync(boat.Id, boat);
        }
    
        public async Task<Boat> GetByIdAsync(int id)
        {
            return await _service.GetByIdAsync(id);
        }
    }
}
