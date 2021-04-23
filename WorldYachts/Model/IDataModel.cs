using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldYachts.Model
{
    public interface IDataModel<TData>
    {
        public TData LastAddedItem { get; set; }
        /// <summary>
        /// Асинхронный метод добавления элемента в БД
        /// </summary>
        /// <returns></returns>
        public Task AddAsync(TData item);
        
        /// <summary>
        /// Асинхронный метод получение списка элементов
        /// </summary>
        /// <returns>Список элементов</returns>
        public Task<IEnumerable<TData>> GetAllAsync();
        
        /// <summary>
        /// Асинхронный метод получение объекта по его Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        public Task<TData> GetByIdAsync(int id);
        /// <summary>
        /// Асинхронный метод удаления лодок из БД
        /// </summary>
        /// <param name="removeItems">Список элементов</param>
        /// <returns></returns>
        public Task DeleteAsync(IEnumerable<TData> removeItems);

        /// <summary>
        /// Сохранение измененной лодки в БД
        /// </summary>
        /// <returns></returns>
        public Task UpdateAsync(TData item);
    }
}
