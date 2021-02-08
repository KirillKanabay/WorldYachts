﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignColors.Recommended;
using WorldYachts.Data;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class UserModel:IDataModel<User>
    {
        public User LastAddedItem { get; set; }
        public IUser LastAddedUser { get; set; }
        public async Task AddAsync(User item)
        {
            await IsRepeated(item);
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await context.Users.AddAsync(item);
                await context.SaveChangesAsync();
                LastAddedItem = item;
            }
        }

        public async Task<IEnumerable<User>> LoadAsync()
        {
            return await Task.Run(() => Load());
        }

        public IEnumerable<User> Load()
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                return context.Users.ToList();
            }
        }

        public async Task RemoveAsync(IEnumerable<User> removeItems)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                context.Users.RemoveRange(removeItems);
                await context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync(User item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                await IsRepeated(item);
                var dbUser = context.Users.FirstOrDefault(u => u.Id == item.Id);

                dbUser.TypeUser = item.TypeUser;
                dbUser.Login = item.Login;
                dbUser.Password = item.Password;
                dbUser.UserId = item.UserId;

                await context.SaveChangesAsync();
            }
        }

        public async Task IsRepeated(User item)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                if (context.Users.Any(u => u.Login == item.Login))
                    throw new ArgumentException("Пользователь с таким логином существует");
            }
            
        }

        public async Task<User> GetItemByIdAsync(int id)
        {
            return await Task.Run((() => GetItemById(id)));
        }

        public User GetItemById(int id)
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                return context.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public async Task LoginAsync(string login, string password)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Login == login && u.Password == password);

                if (user == null)
                {
                    AuthUser.User = null;
                    throw new Exception("Неверный логин или пароль");
                }

                AuthUser.TypeOfUser = (TypeOfUser)user.TypeUser;
                switch (user.TypeUser)
                {
                    case (int)TypeOfUser.Customer:
                        AuthUser.User = context.Customers.SingleOrDefault(u => user.UserId == u.Id);
                        break;
                    case (int)TypeOfUser.SalesPerson:
                        AuthUser.User = context.SalesPersons.SingleOrDefault(u => user.UserId == u.Id);
                        break;
                    case (int)TypeOfUser.Admin:
                        AuthUser.User = context.Admin.SingleOrDefault(u => user.UserId == u.Id);
                        break;
                    default:
                        throw new ArgumentException("Неверный тип пользователя");
                }
            }
        }
        
        public async Task AddCustomerAsync(Customer customer, string login, string password)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                var cm = new CustomerModel();
                
                //Добавление клиента 
                await cm.AddAsync(customer);
                
                //Добавление пользователя
                await AddAsync(new User()
                {
                    Id = default,
                    TypeUser = (int)TypeOfUser.Customer,
                    Login = login,
                    Password = password,
                    UserId = cm.LastAddedItem.Id
                });

                LastAddedUser = cm.LastAddedItem;
            }
        }

        public async Task AddSalesPersonAsync(SalesPerson salesPerson, string login, string password)
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                var spm = new SalesPersonModel();

                //Добавление менеджера
                await spm.AddAsync(salesPerson);

                //Добавление пользователя
                await AddAsync(new User()
                {
                    Id = default,
                    TypeUser = (int)TypeOfUser.SalesPerson,
                    Login = login,
                    Password = password,
                    UserId = spm.LastAddedItem.Id
                });

                LastAddedUser = spm.LastAddedItem;
            }
        }
    }
}
