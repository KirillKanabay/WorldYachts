﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Services.Authenticate;

namespace WorldYachts.Services.Users
{
    public interface IUserService
    {
        Task AuthenticateAsync(AuthenticateRequest request);
        Task<Data.Entities.User> GetByIdAsync(int id);
        Task<IEnumerable<Data.Entities.User>> GetAllAsync();
        Task<Data.Entities.User> AddAsync(Data.Entities.User user);
        Task<Data.Entities.User> UpdateAsync(int id, Data.Entities.User user);
        Task<Data.Entities.User> DeleteAsync(int id);
    }
}
