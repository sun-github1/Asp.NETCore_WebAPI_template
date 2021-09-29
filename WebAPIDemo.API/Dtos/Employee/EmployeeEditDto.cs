using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.Model.Enums;

namespace WebAPIDemo.API.Dtos.Employee
{
    public class EmployeeEditDto
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(10)]
        public string EmployeeName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public Gender Gender { get; set; }
    }
}
