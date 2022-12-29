using Clay.Domain;
using Clay.Domain.Interfaces.Repositories;
using Clay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clay.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ClayDbContext context) : base(context)
        {
        }

        public async Task<Employee> GetByEmailAndPassword(string email, string password)
        {
            return await Entity.SingleOrDefaultAsync(e => e.Password == password && e.Email == email);
        }
    }
}
