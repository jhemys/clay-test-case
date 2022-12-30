using Clay.Domain.Aggregates.Employee;

namespace Clay.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee?> GetByEmailAndPassword(string email, string password);
    }
}
