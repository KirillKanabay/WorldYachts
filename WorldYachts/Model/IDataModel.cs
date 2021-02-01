using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;

namespace WorldYachts.Model
{
    public interface IDataModel<TData>
    {
        /// <summary>
        /// Асинхронный метод добавления элемента в БД
        /// </summary>
        /// <returns></returns>
        public Task AddAsync(TData item);
        
        /// <summary>
        /// Асинхронный метод получение списка элементов
        /// </summary>
        /// <returns>Список элементов</returns>
        public Task<IEnumerable<TData>> LoadAsync();
        
        /// <summary>
        /// Синхронный метод получения списка элементов
        /// </summary>
        /// <returns>Список элементов</returns>
        public IEnumerable<TData> Load();

        /// <summary>
        /// Асинхронный метод удаления лодок из БД
        /// </summary>
        /// <param name="removeItems">Список элементов</param>
        /// <returns></returns>
        public Task RemoveAsync(IEnumerable<TData> removeItems);

        /// <summary>
        /// Сохранение измененной лодки в БД
        /// </summary>
        /// <returns></returns>
        public Task SaveAsync(TData item);

        /// <summary>
        /// Повторность элемента в БД
        /// </summary>
        /// <param name="item">Элемент</param>
        /// <returns></returns>
        public Task IsRepeated(TData item);

        /// <summary>
        /// Асинхронный метод получение объекта по его Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<TData> GetItemByIdAsync(int id);

        /// <summary>
        /// Синхронный метод получения объекта по его Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TData GetItemById(int id);
    }
}
