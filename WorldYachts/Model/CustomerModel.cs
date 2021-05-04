using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.ViewModels;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.DependencyInjections.Services;
using Customer = WorldYachts.Data.Entities.Customer;

namespace WorldYachts.Model
{
    class CustomerModel:ICustomerModel
    {
        public event Func<object, Task> CustomerModelChanged;
        private readonly ICustomerService _customerService;

        public CustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task AddAsync(CustomerUserViewModel customerUserViewModel)
        {
            await _customerService.AddAsync(customerUserViewModel);
            CustomerModelChanged?.Invoke(customerUserViewModel);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerService.GetAllAsync();
        }

        public async Task DeleteAsync(Customer customer)
        {
            await _customerService.DeleteAsync(customer.Id);
            CustomerModelChanged?.Invoke(customer);
        }

        public async Task UpdateAsync(Customer customer)
        {
            await _customerService.UpdateAsync(customer.Id, customer);
            CustomerModelChanged?.Invoke(customer);
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _customerService.GetByIdAsync(id);
        }
    }
}
