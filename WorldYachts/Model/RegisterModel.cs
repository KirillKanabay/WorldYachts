using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldYachts.Annotations;
using WorldYachts.Data;
using WorldYachts.Infrastructure;

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

        public void Register()
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
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
                    TypeUser = (int)TypeUser.Customer,
                    Login = this.Login,
                    Password = this.Password,
                    UserId = userId,
                });
                context.SaveChanges();
            }
        }
    }
}
