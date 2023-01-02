using Clay.Application.Interfaces.Repositories;
using Clay.Domain.Aggregates.Employee;
using Clay.Infrastructure.Data;

namespace Clay.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ClayDbContext context) : base(context)
        {
        }
    }
}
