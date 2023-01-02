using Clay.Domain.DomainObjects.Exceptions;
using DoorHistoryAggregate = Clay.Domain.Aggregates.DoorHistory.DoorHistory;

namespace Clay.Tests.Domain.Aggregates.DoorHistory
{
    public class DoorHistoryTests
    {
        [Fact]
        public void Should_Create_Valid_Entity()
        {
            var doorHistory = DoorHistoryAggregate.Create(1, "Test Door", 1, "Test Employee", "Unlocked", DateTime.UtcNow, null);

            doorHistory.Should().NotBeNull();
        }

        [Theory]
        [InlineData("", "Employee Name", "State", "The parameter DoorName is required.")]
        [InlineData(" ", "Employee Name", "State", "The parameter DoorName is required.")]
        [InlineData(null, "Employee Name", "State", "The parameter DoorName is required.")]
        [InlineData("DoorName", "", "State", "The parameter EmployeeName is required.")]
        [InlineData("DoorName", " ", "State", "The parameter EmployeeName is required.")]
        [InlineData("DoorName", null, "State", "The parameter EmployeeName is required.")]
        [InlineData("DoorName", "Employee Name", "", "The parameter CurrentState is required.")]
        [InlineData("DoorName", "Employee Name", " ", "The parameter CurrentState is required.")]
        [InlineData("DoorName", "Employee Name", null, "The parameter CurrentState is required.")]

        public void Should_Fail_To_Create_Valid_Entity(string doorName, string employeeName, string state, string errorMessage)
        {
            var action = () => DoorHistoryAggregate.Create(1, doorName, 1, employeeName, state, DateTime.UtcNow, null);

            action.Should().Throw<DomainException>().WithMessage(errorMessage);
        }
    }
}
