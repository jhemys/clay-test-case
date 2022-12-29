using Clay.Application.DTOs;
using Clay.Application.Interfaces;
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

        public async Task<EmployeeDTO> GetByEmailAndPassword(string email, string password)
        {
            var employee = await UnitOfWork.EmployeeRepository.GetByEmailAndPassword(email, password);

            return new()
            {
                Email = email,
                Id = employee.Id,
                Name = employee.Name,
                Role = employee.Role
            };
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
