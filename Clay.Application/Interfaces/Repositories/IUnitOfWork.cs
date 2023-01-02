namespace Clay.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IDoorRepository DoorRepository { get; }
        IDoorHistoryRepository DoorHistoryRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        ILoginRepository LoginRepository { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}
