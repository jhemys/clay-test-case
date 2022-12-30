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
            return User.Identity.Name;
        }

        [HttpGet]
        public async Task<IList<EmployeeDTO>> GetAll()
        {
            return await _employeeService.GetAll();
        }

        [HttpGet("{id:int}")]
        public async Task<EmployeeDTO> GetById(int id)
        {
            return await _employeeService.GetById(id);
        }
    }
}
