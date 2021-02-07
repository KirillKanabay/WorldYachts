using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class CustomerModel:IDataModel<Customer>
    {
        public Customer LastAdded { get; set; }
        public async Task AddAsync(Customer item)
        {
            await IsRepeated(item);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await context.Customers.AddAsync(item);
                await context.SaveChangesAsync();
                LastAdded = item;
            }
        }

        public async Task<IEnumerable<Customer>> LoadAsync()
        {
            return await Task.Run(() => Load());
        }

        public IEnumerable<Customer> Load()
        {
            var customersCollection = new List<Customer>();
            using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var customer in context.Customers)
                {
                    customer.Orders = new OrderModel().Load().Where(o=>o.CustomerId == customer.Id).ToList();
                    customersCollection.Add(customer);
                }
            }

            return customersCollection;
        }

        public async Task RemoveAsync(IEnumerable<Customer> removeItems)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                context.Cus.RemoveRange(removeItems);
                await context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync(Customer item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await IsRepeated(item);
                var dbCustomer = context.Customers.FirstOrDefault(c => c.Id == item.Id);

                //Копируем измененного покупателя в БД
                dbCustomer.Id = item.Id;
                dbCustomer.Name = item.Name;
                dbCustomer.SecondName = item.SecondName;
                dbCustomer.BirthDate = item.BirthDate;
                dbCustomer.City = item.City;
                dbCustomer.Address = item.Address;
                dbCustomer.Phone = item.Phone;
                dbCustomer.Email = item.Email;
                dbCustomer.OrganizationName = item.OrganizationName;
                dbCustomer.IdDocumentName = dbCustomer.IdDocumentName;
                dbCustomer.IdNumber = dbCustomer.IdNumber;

                await context.SaveChangesAsync();
            }
        }

        public async Task IsRepeated(Customer item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                if (context.Customers.ToList().Any(c => c.CompareTo(item) == 0))
                {
                    throw new ArgumentException("Такой покупатель уже существует.");
                }
            }
        }

        public async Task<Customer> GetItemByIdAsync(int id)
        {
            return await Task.Run((() => GetItemById(id)));
        }

        public Customer GetItemById(int id)
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                return context.Customers.FirstOrDefault(c => c.Id == id);
            }
        }
    }
}
