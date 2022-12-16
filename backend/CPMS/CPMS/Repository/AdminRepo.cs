using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CPMS.DBConnect;
using CPMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CPMS.Repository
{
    public class AdminRepo : IAdminRepo
    {
        private readonly CPMDbContext cPMDbContext;
        public AdminRepo(CPMDbContext cPMDbContext)
        {
            this.cPMDbContext = cPMDbContext;
        }

        public async Task<Admin> AdminSignIn(string email, string password)
        {
            var admin = await cPMDbContext.Admins.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
            return admin;
        }

        public async Task<bool> CreateAdmin(Admin _Admin)
        {
            var admin = new Admin
            {

                Name = _Admin.Name,
                Email = _Admin.Email,
                Password = _Admin.Password,
                Phone = _Admin.Phone,
                Organization = _Admin.Organization ,              
                ProfileImageName = _Admin.ProfileImageName
            };

            try
            {
                cPMDbContext.Admins.Add(admin);
                await cPMDbContext.SaveChangesAsync();
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public async Task<Admin> GetAdminById(int id)
        {
            var admin = await cPMDbContext.Admins.Where(x => x.Id == id).FirstOrDefaultAsync();
            return admin;

        }
    }
}
