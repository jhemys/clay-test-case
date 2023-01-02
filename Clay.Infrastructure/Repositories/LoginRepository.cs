using Clay.Application.Interfaces.Repositories;
using Clay.Domain.Aggregates.Employee;
using Clay.Domain.Aggregates.Login;
using Clay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clay.Infrastructure.Repositories
{
    public class LoginRepository : GenericRepository<Login>, ILoginRepository
    {
        public LoginRepository(ClayDbContext context) : base(context)
        {
        }

        public async Task<Login?> GetByEmailAndPassword(string email, string password)
        {
            var entity = await Entity.SingleOrDefaultAsync(e =>
                        EF.Functions.Collate(e.Password, "SQL_Latin1_General_CP1_CS_AS") == password &&
                        e.Email == email &&
                        e.IsActive);

            return entity;
        }
    }
}
