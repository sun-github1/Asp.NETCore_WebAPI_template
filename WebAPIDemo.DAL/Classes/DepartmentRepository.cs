using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemo.DAL.DBContext;
using WebAPIDemo.DAL.Interfaces;
using WebAPIDemo.Model.Models;

namespace WebAPIDemo.DAL.Classes
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public ApplicationDbContext _applicationDbConext;
        public DepartmentRepository(ApplicationDbContext applicationDbConext)
        {
            this._applicationDbConext = applicationDbConext;
        }
       
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _applicationDbConext.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentById(int departmentId)
        {
            var department= await _applicationDbConext.Departments.FirstOrDefaultAsync(x => x.DepartmentId == departmentId);

            return department;
        }

    }
}
