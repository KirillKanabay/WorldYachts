using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.Data.ViewModels;

namespace WorldYachts.DependencyInjections.Models
{
    public interface ICustomerModel
    {
        event Func<object, Task> CustomerModelChanged;
        Task AddAsync(CustomerUserViewModel customerUserViewModel);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task DeleteAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task<Customer> GetByIdAsync(int id);
    }
}
