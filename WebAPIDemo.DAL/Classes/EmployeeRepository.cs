using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemo.DAL.DBContext;
using WebAPIDemo.DAL.Interfaces;
using WebAPIDemo.Model.Enums;
using WebAPIDemo.Model.Models;

namespace WebAPIDemo.DAL.Classes
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly ApplicationDbContext _appDbContext;

        public EmployeeRepository(ApplicationDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _appDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            var employee = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            return employee;
        }

        public async Task<Employee> AddEmployee(Employee newEmployee)
        {
            var result = await _appDbContext.Employees.AddAsync(newEmployee);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> UpdateEmployee(Employee updatedEmployee)
        {
            var existingEmployee = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == updatedEmployee.EmployeeId);

            if (existingEmployee != null)
            {
                existingEmployee.EmployeeName = updatedEmployee.EmployeeName;
                existingEmployee.Age = updatedEmployee.Age;
                existingEmployee.Email = updatedEmployee.Email;
                existingEmployee.DepartmentId = updatedEmployee.DepartmentId;
                existingEmployee.Gender = updatedEmployee.Gender;
                await _appDbContext.SaveChangesAsync();

                return existingEmployee;
            }

            return null;
        }

        public async Task<Employee> DeleteEmployee(int employeeId)
        {
            var existingEmployee = await _appDbContext.Employees.
                FirstOrDefaultAsync(x => x.EmployeeId == employeeId);

            if (existingEmployee != null)
            {
                _appDbContext.Employees.Remove(existingEmployee);
                await _appDbContext.SaveChangesAsync();
                return existingEmployee;
            }
            else
            {
                return null;
            }
            
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            var employee = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            return employee;
        }

        public async Task<IEnumerable<Employee>> SearchEmployees(string name, Gender? gender)
        {
            IQueryable<Employee> query = _appDbContext.Employees;
            if (!string.IsNullOrEmpty(name))
            {
               query= _appDbContext.Employees.Where(x => x.EmployeeName.
                            ToLower().Contains(name.ToLower()));
            }

            if(gender.HasValue)
            {
                query = query.Where(x => x.Gender==gender.Value);
            }

            return await query.ToListAsync();
            
        }
    }
}
