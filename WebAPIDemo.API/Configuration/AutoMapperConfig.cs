using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.API.Dtos.Department;
using WebAPIDemo.API.Dtos.Employee;
using WebAPIDemo.Model.Models;

namespace WebAPIDemo.API.Configuration
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Employee, EmployeeAddDto>().ReverseMap();
            CreateMap<Employee, EmployeeEditDto>().ReverseMap();
            CreateMap<Employee, EmployeeResultDto>().ReverseMap();

            CreateMap<Department, DepartmentAddDto>().ReverseMap();
            CreateMap<Department, DepartmentEditDto>().ReverseMap();
            CreateMap<Department, DepartmentResultDto>().ReverseMap();
        }
    }
}
