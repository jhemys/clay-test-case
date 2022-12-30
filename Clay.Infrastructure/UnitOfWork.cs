using Clay.Domain.Interfaces.Repositories;
using Clay.Infrastructure.Data;
using Clay.Infrastructure.Repositories;

namespace Clay.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ClayDbContext _dbContext;
        private IEmployeeRepository _employeeRepository;
        public UnitOfWork(ClayDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEmployeeRepository EmployeeRepository => _employeeRepository ??= new EmployeeRepository(_dbContext);

        public async Task CommitAsync() => await _dbContext.SaveChangesAsync();

        public async Task RollbackAsync() => await _dbContext.DisposeAsync();
    }
}
