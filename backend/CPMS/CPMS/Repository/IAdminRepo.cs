using CPMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS.Repository
{
    public interface IAdminRepo
    {
        Task<bool> CreateAdmin(Admin _Admin);
        Task<Admin> GetAdminById(int id);
        Task<Admin> AdminSignIn(string email, string password);
    }
}
