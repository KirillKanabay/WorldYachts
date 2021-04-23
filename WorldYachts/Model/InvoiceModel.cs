using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class InvoiceModel:IDataModel<Invoice>
    {
        public Invoice LastAddedItem { get; set; }
        public async Task AddAsync(Invoice item)
        {
            await IsRepeated(item);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await context.Invoices.AddAsync(item);
                await context.SaveChangesAsync();
                LastAddedItem = item;
            }
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await Task.Run(() => Load());
        }

        public IEnumerable<Invoice> Load()
        {
            var invoiceCollection = new List<Invoice>();
            using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var invoice in context.Invoices.Where(i=>!i.IsDeleted))
                {
                    invoice.Contract = new ContractModel().GetItemById(invoice.ContractId);
                    invoiceCollection.Add(invoice);
                }
            }

            return invoiceCollection;
        }

        public async Task DeleteAsync(IEnumerable<Invoice> removeItems)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var removeItem in removeItems)
                {
                    removeItem.IsDeleted = true;
                    await UpdateAsync(removeItem);
                }
            }
        }

        public async Task UpdateAsync(Invoice item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await IsRepeated(item);
                var dbInvoice = context.Invoices.FirstOrDefault(a => a.Id == item.Id);

                dbInvoice.ContractId = item.ContractId;
                dbInvoice.Settled = item.Settled;
                dbInvoice.Sum = item.Sum;
                dbInvoice.SumInclVat = item.SumInclVat;
                dbInvoice.Date = item.Date;
                dbInvoice.IsDeleted = item.IsDeleted;

                await context.SaveChangesAsync();
            }
        }

        public async Task IsRepeated(Invoice item)
        {
            
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await Task.Run((() => GetItemById(id)));
        }

        public Invoice GetItemById(int id)
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                return context.Invoices.FirstOrDefault(b => b.Id == id);
            }
        }
    }
}
