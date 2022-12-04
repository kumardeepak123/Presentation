using CPMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS.Repository
{
    public interface IEmployeeRepo
    {
         Task<bool> CreateEmployee(Employee employee);
         Task<Employee> GetEmployeeById(int id);
         Task<List<Employee>> GetAllEmployees();

        Task<bool> UpdateEmployee(int id, Employee employee);
        Task<bool> DeleteEmployee(int id);
         Task<List<Employee>> GetsEmployeesForTeamUp();
    }
}
