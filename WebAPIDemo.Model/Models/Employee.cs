using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebAPIDemo.Model.Enums;

namespace WebAPIDemo.Model.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
