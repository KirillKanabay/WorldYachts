using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.DependencyInjections.Models
{
    public interface IInvoiceModel
    {
        event Func<object, Task> InvoiceModelChanged;
        Task AddAsync(Invoice item);
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task DeleteAsync(Invoice item);
        Task UpdateAsync(Invoice item);
        Task<Invoice> GetByIdAsync(int id);
    }
}
