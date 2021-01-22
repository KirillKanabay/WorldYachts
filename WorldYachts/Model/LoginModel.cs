using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldYachts.Annotations;
using WorldYachts.Infrastructure;

namespace WorldYachts.Model
{
    class LoginModel
    {
        private string _login;
        private string _password;
        public LoginModel(string login, string password)
        {
            _login = login;
            _password = password;
        }

        [CanBeNull]
        public bool Authorization()
        {
            using (var context = WorldYachtsContext.GetDataContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Login == _login && u.Password == _password);
                switch (user.TypeUser)
                {
                    case (int)TypeUser.Customer:
                        AuthUser.User = context.Customers.SingleOrDefault(u => user.UserId == u.Id);
                        break;
                    case (int)TypeUser.SalesPerson:
                        AuthUser.User = context.SalesPersons.SingleOrDefault(u => user.UserId == u.Id);
                        break;
                    case (int)TypeUser.Admin:
                        AuthUser.User = context.Admin.SingleOrDefault(u => user.UserId == u.Id);
                        break;
                    default:
                        throw new ArgumentException("Неверный тип пользователя");
                }
            }

            if (AuthUser.User == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
