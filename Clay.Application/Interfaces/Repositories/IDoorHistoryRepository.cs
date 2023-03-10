using Clay.Domain.Aggregates.DoorHistory;

namespace Clay.Application.Interfaces.Repositories
{
    public interface IDoorHistoryRepository : IGenericRepository<DoorHistory>
    {
        Task<(List<DoorHistory>, int total)> GetAllPaged(int? page, int? pageSize);
        Task<(List<DoorHistory>, int total)> GetByDoorIdPaged(int doorId, int? page, int? pageSize);
    }
}
