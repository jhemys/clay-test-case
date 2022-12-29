using Clay.Application.DTOs;

namespace Clay.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> GetByEmailAndPassword(string email, string password);
        string GenerateToken(EmployeeDTO employee);
    }
}
