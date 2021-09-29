using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.Model.Enums;

namespace WebAPIDemo.API.Dtos.Employee
{
    public class EmployeeResultDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public Gender Gender { get; set; }
    }
}
