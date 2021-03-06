﻿using System.Threading.Tasks;
using WorldYachts.Data.Authenticate;
using WorldYachts.DependencyInjections.Services;
using WorldYachts.Services;


namespace WorldYachts.Model
{
    public class UserModel
    {
        private readonly AuthUser _authUser;
        private readonly IUserService _userService;
        public UserModel(IUserService userService, AuthUser authUser)
        {
            _userService = userService;
            _authUser = authUser;
        }
        public async Task LoginAsync(AuthenticateRequest request)
        {
            await _userService.AuthenticateAsync(request);
        }


        //public User LastAddedItem { get; set; }
        //public IUser LastAddedUser { get; set; }
        //public async Task AddAsync(User item)
        //{
        //    await IsRepeated(item);
        //    await using (var context = WorldYachtsContext.GetDataContext())
        //    {
        //        await context.Users.AddAsync(item);
        //        await context.SaveChangesAsync();
        //        LastAddedItem = item;
        //    }
        //}

        //public async Task<IEnumerable<User>> GetAllAsync()
        //{
        //    return await Task.Run(() => Load());
        //}

        //public IEnumerable<User> Load()
        //{
        //    using (var context = WorldYachtsContext.GetDataContext())
        //    {
        //        return context.Users.Where(i=>!i.IsDeleted).ToList();
        //    }
        //}

        //public async Task DeleteAsync(IEnumerable<User> removeItems)
        //{
        //    await using (var context = WorldYachtsContext.GetDataContext())
        //    {
        //        foreach (var removeItem in removeItems)
        //        {
        //            removeItem.IsDeleted = true;
        //            await UpdateAsync(removeItem);
        //        }
        //    }
        //}

        //public async Task UpdateAsync(User item)
        //{
        //    await using (var context = WorldYachtsContext.GetDataContext())
        //    {
        //        await IsRepeated(item);
        //        var dbUser = context.Users.FirstOrDefault(u => u.Id == item.Id);

        //        dbUser.TypeUser = item.TypeUser;
        //        dbUser.Login = item.Login;
        //        dbUser.Password = item.Password;
        //        dbUser.UserId = item.UserId;
        //        dbUser.IsDeleted = item.IsDeleted;

        //        await context.SaveChangesAsync();
        //    }
        //}

        //public async Task IsRepeated(User item)
        //{
        //    await using (var context = WorldYachtsContext.GetDataContext())
        //    {
        //        if (context.Users.Any(u => u.Login == item.Login))
        //            throw new ArgumentException("Пользователь с таким логином существует");
        //    }
            
        //}

        //public async Task<User> GetByIdAsync(int id)
        //{
        //    return await Task.Run((() => GetItemById(id)));
        //}

        //public User GetItemById(int id)
        //{
        //    using (var context = WorldYachtsContext.GetDataContext())
        //    {
        //        var item = context.Users.FirstOrDefault(u => u.Id == id);
        //        return item;
        //    }
        //}

        
        //public async Task AddCustomerAsync(Customer customer, string login, string password)
        //{
        //    await using (var context = WorldYachtsContext.GetDataContext())
        //    {
        //        var cm = new CustomerModel();
                
        //        //Добавление клиента 
        //        await cm.AddAsync(customer);
                
        //        //Добавление пользователя
        //        await AddAsync(new User()
        //        {
        //            Id = default,
        //            TypeUser = (int)TypeOfUser.Customer,
        //            Login = login,
        //            Password = password,
        //            UserId = cm.LastAddedItem.Id
        //        });

        //        //LastAddedUser = cm.LastAddedItem;
        //    }
        //}

        //public async Task AddSalesPersonAsync(SalesPerson salesPerson, string login, string password)
        //{
        //    await using (var context = WorldYachtsContext.GetDataContext())
        //    {
        //        var spm = new SalesPersonModel();

        //        //Добавление менеджера
        //        //await spm.AddAsync(salesPerson);

        //        //Добавление пользователя
        //        await AddAsync(new User()
        //        {
        //            Id = default,
        //            TypeUser = (int)TypeOfUser.SalesPerson,
        //            Login = login,
        //            Password = password,
        //            UserId = spm.LastAddedItem.Id
        //        });

        //        LastAddedUser = spm.LastAddedItem;
        //    }
        //}
    }
}
