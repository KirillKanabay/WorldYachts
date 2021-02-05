using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorldYachts.Annotations;
using WorldYachts.Infrastructure;
using WorldYachts.Model;

namespace WorldYachts.Data
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
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <returns>Строка состояния авторизации</returns>
        public async Task LoginAsync()
        {
            await using (var context = WorldYachtsContext.GetDataContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Login == _login && u.Password == _password);

                if (user == null)
                {
                    AuthUser.User = null;
                    throw new Exception("Неверный логин или пароль");
                }
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
    }
}
