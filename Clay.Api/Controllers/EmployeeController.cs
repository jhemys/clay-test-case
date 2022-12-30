using Clay.Api.Models;
using Clay.Application.DTOs;
using Clay.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clay.Api.Controllers
{
    //TODO CREATE MODEL FOR RESPONSES
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Test()
        {
            //return User.Identity.Name;
            return "a000";
        }

        [HttpGet]
        public async Task<IList<EmployeeDTO>> GetAll()
        {
            return await _employeeService.GetAll();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeDTO>> GetById(int id)
        {
            return await _employeeService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmployeeRequest request)
        {
            var employeeToCreate = new EmployeeDTO
            {
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
                Role = request.Role
            };

            await _employeeService.CreateEmployee(employeeToCreate);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EmployeeRequest request)
        {
            var employeeToUpdate = new EmployeeDTO
            {
                Id = id,
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
                Role = request.Role
            };

            await _employeeService.UpdateEmployee(employeeToUpdate);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _employeeService.DeleteEmployee(id);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> ChangePassword(int id, [FromBody] ChangePasswordRequest request)
        {
            var employeeToChangePassword = new ChangeEmployeePasswordDTO
            {
                Id = id,
                CurrentPassword = request.CurrentPassword,
                NewPassword = request.NewPassword
            };

            await _employeeService.ChangeEmployeePassword(employeeToChangePassword);

            return NoContent();
        }
    }
}
