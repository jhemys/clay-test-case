using Clay.Domain.Interfaces.Repositories;

namespace Clay.Infrastructure
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}
