using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPIDemo.API.Dtos.Employee;
using WebAPIDemo.DAL.Interfaces;
using WebAPIDemo.Model.Enums;
using WebAPIDemo.Model.Models;

namespace WebAPIDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeerepository;
        private readonly ILogger<EmployeesController> _logger;
        private IMapper _mapper;

        public EmployeesController(ILogger<EmployeesController> logger,
            IEmployeeRepository employeerepository,
            IMapper mapper)
        {
            _logger = logger;
            _employeerepository = employeerepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "EmployeeList")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                _logger.LogTrace("GetEmployees call received");
                var employees = await _employeerepository.GetEmployees();
                return Ok(_mapper.Map<IEnumerable<EmployeeResultDto>>(employees));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching GetEmployees");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            try
            {
                _logger.LogTrace("GetEmployeeById call received");
                var employee = await _employeerepository.GetEmployeeById(id);
                if(employee!=null)
                {
                    return Ok(_mapper.Map<EmployeeResultDto>(employee));
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching GetEmployeeById");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name, Gender? gender)
        {
            try
            {
                _logger.LogTrace("Search call received");
                var employees = await _employeerepository.SearchEmployees(name,gender);
                if (employees != null)
                {
                    return Ok(_mapper.Map<IEnumerable<EmployeeResultDto>>(employees));
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Search");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody]EmployeeAddDto employeetoadd)
        {
            try
            {
                _logger.LogTrace("AddEmployee call received");

                if(employeetoadd == null)
                {
                    return BadRequest();
                }
                var employee = _mapper.Map<Employee>(employeetoadd);
                var employeewithemail= await _employeerepository.GetEmployeeByEmail(employee.Email);

                if (employeewithemail != null)
                {
                    ModelState.AddModelError("Email","Employee with same email id alreday exists");
                    return BadRequest(ModelState);
                }

                var addedemployee = await _employeerepository.AddEmployee(employee);
                if (addedemployee == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,"Failed to add employee");
                }
                else
                {
                    //Add a Location header to the response. The Location header specifies the URI of the newly created employee object
                    return CreatedAtAction(nameof(GetEmployeeById),
                        new { id= addedemployee.EmployeeId},
                        addedemployee);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding Employee");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding employee");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee([FromBody] EmployeeEditDto employeeedit)
        {
            try
            {
                _logger.LogTrace("UpdateEmployee call received");

                if (employeeedit == null)
                {
                    return BadRequest();
                }
                var employee = _mapper.Map<Employee>(employeeedit);

                var existingEmployee = await _employeerepository.GetEmployeeById(employee.EmployeeId);

                if (existingEmployee == null)
                {
                    return NotFound($"Employee with id {employee.EmployeeId} not found");
                }

                var updatedemployee = await _employeerepository.UpdateEmployee(employee);
                if (updatedemployee == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update employee");
                }
                else
                {
                    //Add a Location header to the response. The Location header specifies the URI of the newly created employee object
                    return CreatedAtAction(nameof(GetEmployeeById),
                        new { id = updatedemployee.EmployeeId },
                        updatedemployee);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating Employee");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating employee");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int employeeId)
        {
            try
            {
                _logger.LogTrace("DeleteEmployee call received");

                if (employeeId <= 0)
                {
                    return BadRequest();
                }

                var existingEmployee = await _employeerepository.GetEmployeeById(employeeId);

                if (existingEmployee == null)
                {
                    return NotFound($"Employee with id {employeeId} not found for deletion");
                }

                var deletedemployee = await _employeerepository.DeleteEmployee(employeeId);
                if (deletedemployee == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete employee");
                }
                else
                {
                    return Ok(deletedemployee);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Employee");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting employee");
            }
        }
    }
}
