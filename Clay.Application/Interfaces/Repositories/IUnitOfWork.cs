namespace Clay.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IDoorRepository DoorRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}
