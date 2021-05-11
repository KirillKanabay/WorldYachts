using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.DependencyInjections.Services;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class InvoiceModel:IInvoiceModel
    {
        public event Func<object, Task> InvoiceModelChanged;

        private readonly IInvoiceService _invoiceService;

        public InvoiceModel(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async Task AddAsync(Invoice item)
        {
            await _invoiceService.AddAsync(item);
            InvoiceModelChanged?.Invoke(item);
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _invoiceService.GetAllAsync();
        }

        public async Task DeleteAsync(Invoice Invoice)
        {
            await _invoiceService.DeleteAsync(Invoice.Id);
            InvoiceModelChanged?.Invoke(Invoice);
        }

        public async Task UpdateAsync(Invoice item)
        {
            await _invoiceService.UpdateAsync(item.Id, item);
            InvoiceModelChanged?.Invoke(item);
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await _invoiceService.GetByIdAsync(id);
        }
    }
}
