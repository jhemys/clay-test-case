using Clay.Api.Models;
using Clay.Application.DTOs;
using Clay.Application.Interfaces.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "FullAccess")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IList<EmployeeResponse>> GetAll()
        {
            var employees = await _employeeService.GetAll();

            return employees.Adapt<IList<EmployeeResponse>>();
        }

        [HttpGet("{id:int}")]
        public async Task<EmployeeResponse> GetById(int id)
        {
            var employee = await _employeeService.GetById(id);

            return employee.Adapt<EmployeeResponse>();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EmployeeRequest request)
        {
            var employee = request.Adapt<EmployeeDTO>();
            employee.Id = id;

            await _employeeService.UpdateEmployee(employee);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _employeeService.DeleteEmployee(id);

            return NoContent();
        }
    }
}
