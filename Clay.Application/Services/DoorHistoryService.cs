using Clay.Application.Interfaces.Repositories;
using Clay.Application.Interfaces.Services;
using Clay.Domain.Aggregates.DoorHistory;

namespace Clay.Application.Services
{
    internal class DoorHistoryService : ApplicationBaseService, IDoorHistoryService
    {
        public DoorHistoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<(List<DoorHistory>, int total)> GetAllPaged(int? page, int? pageSize)
        {
            (var overview, var total) = await UnitOfWork.DoorHistoryRepository.GetAllPaged(page, pageSize);

            return (overview, total);
        }

        public async Task<(List<DoorHistory>, int total)> GetByDoorIdPaged(int doorId, int? page, int? pageSize)
        {
            (var overview, var total) = await UnitOfWork.DoorHistoryRepository.GetByDoorIdPaged(doorId, page, pageSize);

            return (overview, total);
        }
    }
}
