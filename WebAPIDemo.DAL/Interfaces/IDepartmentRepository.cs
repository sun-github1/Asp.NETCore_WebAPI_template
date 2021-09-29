using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemo.Model.Models;

namespace WebAPIDemo.DAL.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartmentById(int departmentId);
    }
}
