using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Annotations;
using WorldYachts.Data;
using WorldYachts.Infrastructure;
using WorldYachts.Model;

namespace WorldYachts.Data
{
    class RegisterModel
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public string OrganizationName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IdDocumentName { get; set; }
        public string IdNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public async Task RegisterAsync()
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                //Проверка уникальности логина
                if (context.Users.Any(c => c.Login == Login))
                    throw new ArgumentException("Пользователь с таким логином существует");
                //Проверка уникальности почты
                if (context.Customers.Any(c=>c.Email == Email))
                    throw new ArgumentException("Пользователь с такой почтой уже существует.");
                //Проверка уникальности серии документа
                if(context.Customers.Any(c=>c.IdNumber == IdNumber))
                    throw new ArgumentException("Пользователь с такими документами уже существует");
                //Проверка уникальности номера телефона
                if(context.Customers.Any(c=>c.Phone == Phone))
                    throw new ArgumentException("Пользователь с таким номером телефона уже существует");

                var user = context.Customers.Add(new Customer()
                {
                    Name = this.Name,
                    SecondName = this.SecondName,
                    Address = this.Address,
                    City = this.City,
                    BirthDate = this.BirthDate,
                    Email = this.Email,
                    IdDocumentName = this.IdDocumentName,
                    OrganizationName = this.OrganizationName,
                    IdNumber = this.IdNumber,
                    Phone = this.Phone
                });
                context.SaveChanges();
                var userId = user.Entity.Id;
                context.Users.Add(new User()
                {
                    TypeUser = (int)TypeOfUser.Customer,
                    Login = this.Login,
                    Password = this.Password,
                    UserId = userId,
                });
                context.SaveChanges();
                AuthUser.User = user.Entity;
            }
        }
    }
}
