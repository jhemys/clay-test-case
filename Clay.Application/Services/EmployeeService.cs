using Clay.Application.DTOs;
using Clay.Application.Exceptions;
using Clay.Application.Interfaces.Repositories;
using Clay.Application.Interfaces.Services;
using Clay.Domain.Aggregates.Employee;
using Mapster;

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

            return employees.Adapt<IList<EmployeeDTO>>();
        }

        public async Task<EmployeeDTO> GetById(int id)
        {
            var employee = await FindById(id);

            return employee.Adapt<EmployeeDTO>();
        }

        public async Task UpdateEmployee(EmployeeDTO employee)
        {
            var employeeToUpdate = await FindById(employee.Id);

            employeeToUpdate.SetName(employee.Name);
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
    }
}
