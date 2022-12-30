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

        public async Task<IList<T>> GetAll()
        {
            return await Entity.ToListAsync();
        }

        public async Task<T> GetById(int id)
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
