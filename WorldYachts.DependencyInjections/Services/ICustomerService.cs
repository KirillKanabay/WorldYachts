using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.Data.ViewModels;

namespace WorldYachts.DependencyInjections.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> AddAsync(CustomerUserViewModel customerUserViewModel);
        Task<Customer> UpdateAsync(int id, Customer customer);
        Task<Customer> DeleteAsync(int id);
    }
}
