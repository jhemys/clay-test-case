using Clay.Application.Interfaces.Repositories;
using Clay.Domain.Aggregates.DoorHistory;
using Clay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clay.Infrastructure.Repositories
{
    public class DoorHistoryRepository : GenericRepository<DoorHistory>, IDoorHistoryRepository
    {
        public int PageSize { get; set; } = 50;
        public int PageNumber { get; set; } = 1;
        public DoorHistoryRepository(ClayDbContext context) : base(context) { }

        public async Task<(List<DoorHistory>, int total)> GetAllPaged(int? page, int? pageSize)
        {
            var overview = await Entity
                                    .Skip(GetPageSize(pageSize) * (GetPage(page) - 1))
                                    .Take(GetPageSize(pageSize))
                                    .ToListAsync();

            var total = Entity.Count();

            return (overview, total);
        }

        public async Task<(List<DoorHistory>, int total)> GetByDoorIdPaged(int doorId, int? page, int? pageSize)
        {
            var overview = await Entity
                                    .Where(x => x.DoorId == doorId)
                                    .Skip(GetPageSize(pageSize) * (GetPage(page) - 1))
                                    .Take(GetPageSize(pageSize))
                                    .ToListAsync();

            var total = Entity
                            .Where(x => x.DoorId == doorId).Count();

            return (overview, total);
        }

        private int GetPageSize(int? pageSize)
        {
            return pageSize == null || pageSize.Value <= 0 ? PageSize : pageSize.GetValueOrDefault();
        }

        private int GetPage(int? page)
        {
            return page == null || page.Value <= 0 ? PageNumber : page.GetValueOrDefault();
        }
    }
}
