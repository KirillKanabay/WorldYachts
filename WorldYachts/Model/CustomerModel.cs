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
        public Customer LastAddedItem { get; set; }
        public async Task AddAsync(Customer item)
        {
            await IsRepeated(item);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await context.Customers.AddAsync(item);
                await context.SaveChangesAsync();
                LastAddedItem = item;
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await Task.Run(() => Load());
        }

        public IEnumerable<Customer> Load()
        {
            var customersCollection = new List<Customer>();
            using (var context = WorldYachtsContext.GetDataContext())
            {
                foreach (var customer in context.Customers.Where(i=>!i.IsDeleted))
                {
                    customer.Orders = new OrderModel().Load().Where(o=>o.CustomerId == customer.Id).ToList();
                    customersCollection.Add(customer);
                }
            }

            return customersCollection;
        }

        public async Task DeleteAsync(IEnumerable<Customer> removeItems)
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

        public async Task UpdateAsync(Customer item)
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
                dbCustomer.IsDeleted = item.IsDeleted;

                await context.SaveChangesAsync();
            }
        }

        public async Task IsRepeated(Customer item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                //Проверка уникальности почты
                if (context.Customers.Any(c => c.Email == item.Email))
                    throw new ArgumentException("Клиент с такой почтой уже существует.");
                //Проверка уникальности серии документа
                if (context.Customers.Any(c => c.IdNumber == item.IdNumber))
                    throw new ArgumentException("Клиент с такими документами уже существует");
                //Проверка уникальности номера телефона
                if (context.Customers.Any(c => c.Phone == item.Phone))
                    throw new ArgumentException("Клиент с таким номером телефона уже существует");
            }
        }

        public async Task<Customer> GetByIdAsync(int id)
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
