using Clay.Application.Interfaces.Repositories;
using Clay.Domain.Aggregates.Door;
using Clay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clay.Infrastructure.Repositories
{
    public class DoorRepository : GenericRepository<Door>, IDoorRepository
    {
        public DoorRepository(ClayDbContext context) : base(context) { }

        public override async Task<IList<Door>> GetAll()
        {
            return await Entity.Include(x => x.AllowedRoles).ToListAsync();
        }

        public override async Task<Door?> GetById(int id)
        {
            return await Entity
                            .Include(x => x.AllowedRoles.Where(role => role.IsActive))
                            .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddRole(Role role)
        {
            await Context.Set<Role>().AddAsync(role);
        }

        public async Task<Role?> GetRoleByName(string name)
        {
            return await Context.Set<Role>().SingleOrDefaultAsync(role => role.Name == name);
        }
    }
}
