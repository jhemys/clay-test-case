using Clay.Domain.Aggregates.Login;

namespace Clay.Application.Interfaces.Repositories
{
    public interface ILoginRepository : IGenericRepository<Login>
    {
        Task<Login?> GetByEmailAndPassword(string email, string password);
    }
}
