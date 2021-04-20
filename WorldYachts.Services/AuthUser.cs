using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WorldYachts.Services.Authenticate;

namespace WorldYachts.Services
{
    public class AuthUser
    {
        #region Singleton
        private AuthUser(AuthenticateResponse response)
        {
            Username = response.Username;
            Email = response.Email;
            Role = response.Role;
            UserId = response.UserId;
            Token = response.Token;

            WebClientWorker.GetInstance().Token = Token;
        }

        private static AuthUser _instance;

        private static readonly object _lock = new object();

        public static AuthUser GetInstance(AuthenticateResponse response)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new AuthUser(response);
                }
            }

            return _instance;
        }

        public static AuthUser GetInstance()
        {
            return _instance;
        }
        #endregion

        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
