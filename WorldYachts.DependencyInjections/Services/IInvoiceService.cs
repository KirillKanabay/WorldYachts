using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.DependencyInjections.Services
{
    public interface IInvoiceService
    {
        Task<Invoice> GetByIdAsync(int id);
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<Invoice> AddAsync(Invoice invoice);
        Task<Invoice> UpdateAsync(int id, Invoice invoice);
        Task<Invoice> DeleteAsync(int id);
    }
}
