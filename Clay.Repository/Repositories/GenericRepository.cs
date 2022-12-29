using Clay.Domain.Core.DomainObjects;
using Clay.Domain.Interfaces.Repositories;
using Clay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clay.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly ClayDbContext _context;
        protected readonly DbSet<T> Entity;
        public GenericRepository(ClayDbContext context)
        {
            _context = context;
            Entity = _context.Set<T>();
        }

        public async Task AddAsync(T model)
        {
            await Entity.AddAsync(model);
            await _context.SaveEntitiesAsync();
        }

        public async Task<T> GetById(long id)
        {
            return await Entity.SingleOrDefaultAsync(x => x.Id == id);
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

            await _context.SaveEntitiesAsync();
        }

        public void Update(T model)
        {
            Entity.Attach(model);
            Entity.Entry(model).State = EntityState.Modified;
        }
    }
}
