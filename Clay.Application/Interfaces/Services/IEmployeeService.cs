using Clay.Application.DTOs;

namespace Clay.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<IList<EmployeeDTO>> GetAll();
        Task<EmployeeDTO> GetById(int id);
        Task UpdateEmployee(EmployeeDTO employee);
        Task DeleteEmployee(int id);
    }
}
