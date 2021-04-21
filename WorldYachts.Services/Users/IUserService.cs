using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorldYachts.Services.Users
{
    public interface IUserService
    {
        Task AuthenticateAsync(string username, string password);
    }
}
