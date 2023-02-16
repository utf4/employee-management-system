using Employ.Business.Services;
using EmployeeManagmentSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EmployeeManagmentSystem.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet, Route("get-employees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _employeeService.GetAllEmploy();
        }
        [HttpGet("{id}"), Route("get-employees-by-id")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmpById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost, Route("create-Employee")]
        public async Task<ActionResult<bool>> PostEmployee(Employee employee)
        {
            var result = await _employeeService.CreateEmploy(employee);
            if (result is true)
                return Ok();
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}"),Route("edit-employe")]
        public async Task<IActionResult> PutEmployee(Employee employee)
        {
            await _employeeService.EditEmploy(employee);
            return Ok();
        }

        [HttpDelete("{id}"),Route("Delete-employee")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
           await _employeeService.DeleteEmploy(id);
           return Ok(); 
        }
        [HttpGet("search"),Route("Search-Employee")]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchEmployees(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("The 'query' parameter is required.");
            }
            var employees = await _employeeService.SearchByNameOrEmail(query);

            if (!employees.Any())
            {
                return NotFound($"No employees found matching the search query '{query}'.");
            }

            return employees;
        }

    }
}
