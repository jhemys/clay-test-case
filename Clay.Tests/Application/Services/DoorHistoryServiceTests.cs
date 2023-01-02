using Clay.Application.Interfaces.Repositories;
using Clay.Application.Services;
using Clay.Domain.Aggregates.DoorHistory;

namespace Clay.Tests.Application.Services
{
    public class DoorHistoryServiceTests
    {
        [Fact]
        public async Task GetAllPaged_Should_Return_Data()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorHistoryService(unitOfWork);
            var doorHistories = PrepareData();
            A.CallTo(() => unitOfWork.DoorHistoryRepository.GetAllPaged(A<int?>.Ignored, A<int?>.Ignored)).Returns(doorHistories);

            (var data, var total) = await service.GetAllPaged(1, 50);

            total.Should().Be(2);
            data.Count.Should().Be(2);
        }

        [Fact]
        public async Task GetByDoorIdPaged_Should_Return_Data()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorHistoryService(unitOfWork);
            var doorHistories = PrepareData();
            A.CallTo(() => unitOfWork.DoorHistoryRepository.GetByDoorIdPaged(A<int>.Ignored, A<int?>.Ignored, A<int?>.Ignored)).Returns(doorHistories);

            (var data, var total) = await service.GetByDoorIdPaged(1, 1, 50);

            total.Should().Be(2);
            data.Count.Should().Be(2);
        }

        private (List<DoorHistory> doorHistories, int total) PrepareData()
        {
            var doorHistories = new List<DoorHistory>
            {
                DoorHistory.Create(1, "Test", 1, "Employee", "Unlocked", DateTime.UtcNow, null),
                DoorHistory.Create(1, "Test", 2, "Employee 2", "UnlockFailed", DateTime.UtcNow, null, "error")
            };

            return (doorHistories, 2);
        }
    }
}
