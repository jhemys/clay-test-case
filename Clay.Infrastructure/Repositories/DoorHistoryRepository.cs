using Clay.Application.Interfaces.Repositories;
using Clay.Domain.Aggregates.DoorHistory;
using Clay.Infrastructure.Data;

namespace Clay.Infrastructure.Repositories
{
    public class DoorHistoryRepository : GenericRepository<DoorHistory>, IDoorHistoryRepository
    {
        public DoorHistoryRepository(ClayDbContext context) : base(context) { }
    }
}
