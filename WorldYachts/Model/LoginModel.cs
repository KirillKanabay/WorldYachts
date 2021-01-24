using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldYachts.Annotations;
using WorldYachts.Infrastructure;

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
        public string Authorization()
        {
            string errorMessage = "OK";
            try
            {
                var context = WorldYachtsContext.GetDataContext();
                var user = context.Users.SingleOrDefault(u => u.Login == _login && u.Password == _password);

                if (user == null)
                {
                    errorMessage = "Неверный логин или пароль";
                }
                else
                {
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
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }

            return errorMessage;
        }
    }
}
