using Clay.Application.DTOs;
using Clay.Application.Exceptions;
using Clay.Application.Interfaces.Repositories;
using Clay.Application.Interfaces.Services;
using Clay.Domain.Aggregates.Employee;
using Clay.Domain.Aggregates.Login;
using Clay.Domain.DomainObjects.Exceptions;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clay.Application.Services
{
    public class LoginService : ApplicationBaseService, ILoginService
    {
        public LoginService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<LoginDTO> GetByEmailAndPassword(string email, string password)
        {
            var login = await UnitOfWork.LoginRepository.GetByEmailAndPassword(email, password);

            if (login is null)
                throw new EntityNotFoundException();

            return login.Adapt<LoginDTO>();
        }

        public async Task ChangePassword(ChangePasswordDTO login)
        {
            var loginToUpdate = await FindById(login.Id);

            var currentLoginPermissionType = PermissionType.Employee;
            if(!Enum.TryParse(login.CurrentLoginPermissionType, out currentLoginPermissionType))
                throw new DomainActionNotPermittedException();

            loginToUpdate.ChangePassword(login.CurrentLoginId, currentLoginPermissionType, login.CurrentPassword, login.NewPassword);

            UnitOfWork.LoginRepository.Update(loginToUpdate);

            await UnitOfWork.CommitAsync();
        }

        public string GenerateToken(LoginDTO login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET");
            byte[] key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", login.Id.ToString()),
                    new Claim(ClaimTypes.Name, login.Email.ToString()),
                    new Claim(ClaimTypes.Role, login.PermissionType.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<Login> FindById(int id)
        {
            var login = await UnitOfWork.LoginRepository.GetById(id);

            if (login is null)
                throw new EntityNotFoundException();

            return login;
        }

        public async Task CreateLogin(LoginDTO login)
        {
            var employeeToCreate = Employee.Create(login.Name, login.Role, login.TagIdentification);
            
            var loginToCreate = Login.Create(login.Email, login.Password, employeeToCreate, login.PermissionType);

            await UnitOfWork.EmployeeRepository.AddAsync(employeeToCreate);
            await UnitOfWork.LoginRepository.AddAsync(loginToCreate);

            await UnitOfWork.CommitAsync();
        }
    }
}
