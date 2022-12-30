using Clay.Application.Interfaces.Repositories;

namespace Clay.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}
