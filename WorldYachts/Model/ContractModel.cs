﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class ContractModel:IDataModel<Contract>
    {
        public Contract LastAddedItem { get; set; }
        public async Task AddAsync(Contract item)
        {
            await IsRepeated(item);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await context.Contracts.AddAsync(item);
                await context.SaveChangesAsync();
                LastAddedItem = item;
            }
        }

        public async Task<IEnumerable<Contract>> LoadAsync()
        {
            return await Task.Run(() => Load());
        }

        public IEnumerable<Contract> Load()
        {
            var contractCollection = new List<Contract>();
            using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var contract in context.Contracts)
                {
                    contract.Order = new OrderModel().GetItemById(contract.OrderId);
                    contractCollection.Add(contract);
                }
            }

            return contractCollection;
        }

        public async Task RemoveAsync(IEnumerable<Contract> removeItems)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                context.Contracts.RemoveRange(removeItems);
                await context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync(Contract item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await IsRepeated(item);
                var dbContract = context.Contracts.FirstOrDefault(a => a.Id == item.Id);

                dbContract.OrderId = item.OrderId;
                dbContract.Date = item.Date;
                dbContract.DepositPayed = item.DepositPayed;
                dbContract.ContractTotalPrice = item.ContractTotalPrice;
                dbContract.ContractTotalPriceInclVat = item.ContractTotalPriceInclVat;
                dbContract.ProductionProcess = item.ProductionProcess;

                await context.SaveChangesAsync();
            }
        }

        public async Task IsRepeated(Contract item)
        {
            
        }

        public async Task<Contract> GetItemByIdAsync(int id)
        {
            return await Task.Run((() => GetItemById(id)));
        }

        public Contract GetItemById(int id)
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                var item = context.Contracts.FirstOrDefault(c => c.Id == id);
                item.Order = new OrderModel().GetItemById(item.OrderId);
                return item;
            }
        }
    }
}
