using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDemo.API.Dtos.Department
{
    public class DepartmentAddDto
    {
        [Required]
        [StringLength(10)]
        public string DepartmentName { get; set; }
    }
}
