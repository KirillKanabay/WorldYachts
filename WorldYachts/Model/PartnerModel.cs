using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class PartnerModel : IDataModel<Partner>
    {
        public Partner LastAddedItem { get; set; }

        /// <summary>
        /// Асинхронный метод добавления партнера в БД
        /// </summary>
        /// <param name="partner">Добавляемый партнер</param>
        /// <returns></returns>
        public async Task AddAsync(Partner partner)
        {
            await IsRepeated(partner);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await context.Partners.AddAsync(partner);
                await context.SaveChangesAsync();

                LastAddedItem = partner;
            }
        }
        /// <summary>
        /// Асинхронный метод загрузки партнеров из БД
        /// </summary>
        /// <returns>Коллекция партнеров</returns>
        public async Task<IEnumerable<Partner>> GetAllAsync()
        {
            return await Task.Run(() => Load());
        }
        /// <summary>
        /// Синхронный метод загрузки партнеров из БД
        /// </summary>
        /// <returns>Коллекция партнеров</returns>
        public IEnumerable<Partner> Load()
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                return context.Partners.Where(i=>!i.IsDeleted).ToList();
            }
        }
        /// <summary>
        /// Асинхронный метод удаления коллекции партнеров из БД
        /// </summary>
        /// <param name="removePartners">Коллекция партнеров</param>
        /// <returns></returns>
        public async Task DeleteAsync(IEnumerable<Partner> removePartners)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var removePartner in removePartners)
                {
                    removePartner.IsDeleted = true;
                    await UpdateAsync(removePartner);
                }
            }
        }
        /// <summary>
        /// Асинхронный метод изменения партнера 
        /// </summary>
        /// <param name="partner">Партнер</param>
        /// <returns></returns>
        public async Task UpdateAsync(Partner partner)
        {
            await IsRepeated(partner);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                var dbPartner = context.Partners.FirstOrDefault(p => p.Id == partner.Id);

                //Копируем измененного партнера в БД
                dbPartner.Name = partner.Name;
                dbPartner.Address = partner.Address;
                dbPartner.City = partner.City;
                dbPartner.IsDeleted = partner.IsDeleted;

                await context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Проверка идентичного партнера в БД
        /// </summary>
        /// <param name="partner"></param>
        /// <returns></returns>
        public async Task IsRepeated(Partner partner)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                if (context.Partners.ToList().Any(c => c.CompareTo(partner) == 0))
                {
                    throw new ArgumentException("Такой уже существует.");
                }
            }
        }

        public async Task<Partner> GetByIdAsync(int id)
        {
            return await Task.Run((() => GetItemById(id)));
        }

        public Partner GetItemById(int id)
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                return context.Partners.FirstOrDefault(p => p.Id == id);
            }
        }
    }
}