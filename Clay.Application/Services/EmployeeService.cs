using Clay.Application.DTOs;
using Clay.Application.Exceptions;
using Clay.Application.Interfaces;
using Clay.Domain;
using Clay.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clay.Application.Services
{
    public class EmployeeService : ApplicationBaseService, IEmployeeService
    {
        public EmployeeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IList<EmployeeDTO>> GetAll()
        {
            var employees = await UnitOfWork.EmployeeRepository.GetAll();

            return employees.Select(employee => new EmployeeDTO
            {
                Email = employee.Email,
                Id = employee.Id,
                Name = employee.Name,
                Role = employee.Role.Name,
                IsActive = employee.IsActive,
            }).ToList();
        }

        public async Task<EmployeeDTO> GetById(int id)
        {
            var employee = await FindById(id);

            return new()
            {
                Email = employee.Email,
                Id = employee.Id,
                Name = employee.Name,
                Role = employee.Role.Name,
                IsActive = employee.IsActive,
            };
        }

        public async Task<EmployeeDTO> GetByEmailAndPassword(string email, string password)
        {
            var employee = await UnitOfWork.EmployeeRepository.GetByEmailAndPassword(email, password);

            if (employee is null)
                throw new EntityNotFoundException();

            return new()
            {
                Email = email,
                Id = employee.Id,
                Name = employee.Name,
                Role = employee.Role.Name,
                IsActive = employee.IsActive,
            };
        }

        public async Task CreateEmployee(EmployeeDTO employee)
        {

            var employeeToCreate = Employee.Create(employee.Name, employee.Role, employee.Password, employee.Email);

            await UnitOfWork.EmployeeRepository.AddAsync(employeeToCreate);

            await UnitOfWork.CommitAsync();
        }

        public async Task UpdateEmployee(EmployeeDTO employee)
        {
            var employeeToUpdate = await FindById(employee.Id);

            employeeToUpdate.SetName(employee.Name);
            employeeToUpdate.SetEmail(employee.Email);
            employeeToUpdate.SetRole(employee.Role);

            UnitOfWork.EmployeeRepository.Update(employeeToUpdate);

            await UnitOfWork.CommitAsync();
        }

        public async Task DeleteEmployee(int id)
        {
            var employeeToDelete = await FindById(id);

            employeeToDelete.SetIsActive(false);

            UnitOfWork.EmployeeRepository.Update(employeeToDelete);

            await UnitOfWork.CommitAsync();
        }

        private async Task<Employee> FindById(int id)
        {
            var employeeToUpdate = await UnitOfWork.EmployeeRepository.GetById(id);

            if (employeeToUpdate is null)
                throw new EntityNotFoundException();

            return employeeToUpdate;
        }

        public string GenerateToken(EmployeeDTO employee)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET");
            byte[] key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", employee.Id.ToString()),
                    new Claim(ClaimTypes.Name, employee.Name.ToString()),
                    new Claim(ClaimTypes.Role, employee.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
