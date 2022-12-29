using Clay.Api.Models;
using Clay.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public LoginController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Authenticate([FromBody] LoginRequest model)
        {
            var employee = await _employeeService.GetByEmailAndPassword(model.Email, model.Password);

            if (employee == null)
                return NotFound();

            var token = _employeeService.GenerateToken(employee);

            return new LoginResponse
            {
                Id = employee.Id,
                Name = employee.Name,
                Token = token
            };
        }
    }
}
