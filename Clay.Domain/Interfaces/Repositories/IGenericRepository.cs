using Clay.Domain.Core.DomainObjects;

namespace Clay.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<TModel> where TModel : Entity
    {
        Task<TModel> GetById(long id);
        Task AddAsync(TModel model);
        void Remove(TModel model);
        void Update(TModel model);
        Task Save(TModel model);
    }
}