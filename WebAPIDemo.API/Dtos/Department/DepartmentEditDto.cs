using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDemo.API.Dtos.Department
{
    public class DepartmentEditDto
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        [StringLength(10)]
        public string DepartmentName { get; set; }
    }
}
