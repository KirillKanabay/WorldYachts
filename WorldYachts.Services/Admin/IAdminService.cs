using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorldYachts.Services.Admin
{
    public interface IAdminService
    {
        Task<Data.Entities.Admin> GetByIdAsync(int id);
    }
}
