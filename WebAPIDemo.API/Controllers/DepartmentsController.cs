using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.API.Dtos.Department;
using WebAPIDemo.DAL.Interfaces;
using WebAPIDemo.Model.Models;

namespace WebAPIDemo.API.Controllers
{
    //public class DepartmentsController : ControllerBase
    //{
    //    public ActionResult Index()
    //    {
    //        return Ok("hello");
    //    }
    //}

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly ILogger<DepartmentsController> _logger;
        private IMapper _mapper;

        public DepartmentsController(IDepartmentRepository departmentRepository, ILogger<DepartmentsController> logger,
            IMapper mapper )
        {
            this.departmentRepository = departmentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            try
            {
                var departments = await departmentRepository.GetDepartments();
                return Ok(_mapper.Map<IEnumerable<DepartmentResultDto>>(departments));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DepartmentResultDto>> GetDepartment(int id)
        {
            try
            {
                var result = await departmentRepository.GetDepartmentById(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<DepartmentResultDto>( result));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
}
