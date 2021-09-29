using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemo.Model.Enums;
using WebAPIDemo.Model.Models;

namespace WebAPIDemo.DAL.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int employeeId);

        Task<Employee> GetEmployeeByEmail(string email);
        Task<Employee> AddEmployee(Employee newEmployee);
        Task<Employee> UpdateEmployee(Employee updatedEmployee);
        Task<Employee> DeleteEmployee(int employeeId);

        Task<IEnumerable<Employee>> SearchEmployees(string name, Gender? gender);
    }
}
