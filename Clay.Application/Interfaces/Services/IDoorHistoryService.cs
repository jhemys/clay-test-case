using Clay.Domain.Aggregates.DoorHistory;

namespace Clay.Application.Interfaces.Services
{
    public interface IDoorHistoryService
    {
        Task<(List<DoorHistory>, int total)> GetAllPaged(int? page, int? pageSize);
        Task<(List<DoorHistory>, int total)> GetByDoorIdPaged(int doorId, int? page, int? pageSize);
    }
}
