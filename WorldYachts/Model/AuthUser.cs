using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.Data;

namespace WorldYachts.Model
{
    [Serializable]
    struct SerializableUserInfo
    {
        public string Login, Password;

        public SerializableUserInfo(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
    
    static class AuthUser
    {
        public static IUser User;
        public static TypeOfUser TypeOfUser;
    }
}
