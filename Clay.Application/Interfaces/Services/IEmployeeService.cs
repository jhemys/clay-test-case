using Clay.Application.DTOs;

namespace Clay.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> GetByEmailAndPassword(string email, string password);
        Task CreateEmployee(EmployeeDTO employee);
        Task<IList<EmployeeDTO>> GetAll();
        Task<EmployeeDTO> GetById(int id);
        Task UpdateEmployee(EmployeeDTO employee);
        Task DeleteEmployee(int id);
        Task ChangeEmployeePassword(ChangeEmployeePasswordDTO employee);
        string GenerateToken(EmployeeDTO employee);
    }
}
