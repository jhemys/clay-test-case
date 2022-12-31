using Clay.Application.Interfaces.Repositories;
using Clay.Domain.DomainObjects;
using Clay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clay.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity, IAggregateRoot
    {
        protected readonly ClayDbContext Context;
        protected readonly DbSet<T> Entity;
        public GenericRepository(ClayDbContext context)
        {
            Context = context;
            Entity = Context.Set<T>();
        }

        public virtual async Task<IList<T>> GetAll()
        {
            return await Entity.ToListAsync();
        }

        public virtual async Task<T?> GetById(int id)
        {
            return await Entity.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(T model)
        {
            await Entity.AddAsync(model);
        }

        public void Remove(T model)
        {
            Entity.Attach(model);
            Entity.Remove(model);
        }

        public async Task Save(T model)
        {
            if (model.Id <= 0)
            {
                await Entity.AddAsync(model);
            }
            else
            {
                Update(model);
            }
        }

        public void Update(T model)
        {
            Entity.Attach(model);
            Entity.Entry(model).State = EntityState.Modified;
        }
    }
}
