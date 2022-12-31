using Clay.Domain.Aggregates.Door;

namespace Clay.Application.Interfaces.Repositories
{
    public interface IDoorRepository : IGenericRepository<Door>
    {
        Task AddRole(Role role);
        Task<Role?> GetRoleByName(string name);
    }
}
