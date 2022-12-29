namespace Clay.Domain.Interfaces.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee> GetByEmailAndPassword(string email, string password);
    }
}
