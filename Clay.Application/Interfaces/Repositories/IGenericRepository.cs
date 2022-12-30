using Clay.Domain.Core.DomainObjects;

namespace Clay.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TModel> where TModel : Entity
    {
        Task<IList<TModel>> GetAll();
        Task<TModel> GetById(int id);
        Task AddAsync(TModel model);
        void Remove(TModel model);
        void Update(TModel model);
        Task Save(TModel model);
    }
}