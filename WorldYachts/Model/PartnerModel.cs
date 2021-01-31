﻿using System;
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
            }
        }
        /// <summary>
        /// Асинхронный метод загрузки партнеров из БД
        /// </summary>
        /// <returns>Коллекция партнеров</returns>
        public async Task<IEnumerable<Partner>> LoadAsync()
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
                return context.Partners.ToList();
            }
        }
        /// <summary>
        /// Асинхронный метод удаления коллекции партнеров из БД
        /// </summary>
        /// <param name="removePartners">Коллекция партнеров</param>
        /// <returns></returns>
        public async Task RemoveAsync(IEnumerable<Partner> removePartners)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                context.Partners.RemoveRange(removePartners);
                await context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Асинхронный метод изменения партнера 
        /// </summary>
        /// <param name="partner">Партнер</param>
        /// <returns></returns>
        public async Task SaveAsync(Partner partner)
        {
            await IsRepeated(partner);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                var dbPartner = context.Partners.FirstOrDefault(p => p.Id == partner.Id);

                //Копируем измененного партнера в БД
                dbPartner.Name = partner.Name;
                dbPartner.Address = partner.Address;
                dbPartner.City = partner.City;

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
    }
}