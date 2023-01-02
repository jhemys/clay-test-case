using Clay.Domain.Aggregates.Door;
using Clay.Domain.DomainObjects.Exceptions;
using DoorAggregate = Clay.Domain.Aggregates.Door.Door;

namespace Clay.Tests.Domain.Aggregates.Door
{
    public class DoorTests
    {
        [Theory]
        [InlineData("description", false, false)]
        [InlineData("description", true, false)]
        [InlineData("description", false, true)]
        [InlineData("description", true, true)]
        [InlineData(null, false, false)]
        [InlineData(null, true, false)]
        [InlineData(null, false, true)]
        [InlineData(null, true, true)]
        public void Should_Create_Valid_Entity(string description, bool isLocked, bool isAccessRestricted)
        {
            var doorEntity = DoorAggregate.Create("Door name", description, isLocked, isAccessRestricted);

            doorEntity.Should().NotBeNull();
        }

        [Theory]
        [InlineData("", "Description", false, false, "The parameter Name is required.")]
        [InlineData(" ", "Description", false, false, "The parameter Name is required.")]
        [InlineData(null, "Description", false, false, "The parameter Name is required.")]
        public void Should_Fail_To_Create_Valid_Entity(string doorName, string description, bool isLocked, bool isAccessRestricted, string errorMessage)
        {
            var action = () => DoorAggregate.Create(doorName, description, isLocked, isAccessRestricted);

            action.Should().Throw<DomainException>().WithMessage(errorMessage);
        }

        [Theory]
        [InlineData(true, "Test Role")]
        [InlineData(false, "Test Role")]
        public void CanBeUnlockedByRole_Should_Return_True(bool isAccessRestricted, string role)
        {
            var doorEntity = DoorAggregate.Create("Door name", null, true, isAccessRestricted);
            if (isAccessRestricted)
                doorEntity.AddRole(Role.Create("Test Role"));

            doorEntity.CanBeUnlockedByRole(Role.Create(role)).Should().BeTrue();
        }

        [Fact]
        public void CanBeUnlockedByRole_Should_Return_False()
        {
            var doorEntity = DoorAggregate.Create("Door name", null, true, true);
            doorEntity.AddRole(Role.Create("Test Role"));

            doorEntity.CanBeUnlockedByRole(Role.Create("Role")).Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void SetName_Should_Fail(string doorName)
        {
            var doorEntity = DoorAggregate.Create("Door name", null, true, true);
            
            var action = () => doorEntity.SetName(doorName);

            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void SetName_Should_Change_Name()
        {
            var doorEntity = DoorAggregate.Create("Door name", null, true, true);

            doorEntity.SetName("Door Name 2");

            doorEntity.Name.Should().Be("Door Name 2");
        }

        [Fact]
        public void SetDescription_Should_Change_Description()
        {
            var doorEntity = DoorAggregate.Create("Door name", null, true, true);

            doorEntity.SetDescription("Description");

            doorEntity.Description.Should().Be("Description");
        }

        [Fact]
        public void SetAccessRestricted_Should_Change_To_True()
        {
            var doorEntity = DoorAggregate.Create("Door name", null, true, false);

            doorEntity.SetAccessRestricted(true);

            doorEntity.IsAccessRestricted.Should().BeTrue();
        }

        [Fact]
        public void SetAccessRestricted_Should_Change_To_False()
        {
            var doorEntity = DoorAggregate.Create("Door name", null, true, true);
            doorEntity.AddRole(Role.Create("role 1"));

            doorEntity.SetAccessRestricted(false);

            doorEntity.IsAccessRestricted.Should().BeFalse();
            doorEntity.AllowedRoles.Should().BeEmpty();
        }

        [Theory]
        [InlineData(false, "role", "Roles can only be added/removed to restricted doors.")]
        [InlineData(true, "role", "Informed role already exists for this door.")]
        public void AddRole_Should_Fail_To_Add(bool isAccessRestricted, string role, string errorMessage)
        {
            var doorEntity = DoorAggregate.Create("Door name", null, true, isAccessRestricted);

            if (isAccessRestricted)
                doorEntity.AddRole(Role.Create(role));

            var action = () => doorEntity.AddRole(Role.Create(role));

            action.Should().Throw<DomainException>().WithMessage(errorMessage);
        }

        [Fact]
        public void AddRole_Should_Add_Role()
        {
            var doorEntity = DoorAggregate.Create("Door name", null, true, true);

            doorEntity.AddRole(Role.Create("Role"));

            doorEntity.AllowedRoles.Should().OnlyContain(x => x.Name == "Role");
        }

        [Theory]
        [InlineData(false, "role", "Roles can only be added/removed to restricted doors.")]
        [InlineData(true, "role", "Informed role does not exist for this door.")]
        public void RemoveRole_Should_Fail_To_Remove(bool isAccessRestricted, string role, string errorMessage)
        {
            var doorEntity = DoorAggregate.Create("Door name", null, true, isAccessRestricted);

            if (isAccessRestricted)
                doorEntity.AddRole(Role.Create("Existing Role"));

            var action = () => doorEntity.RemoveRole(Role.Create(role));

            action.Should().Throw<DomainException>().WithMessage(errorMessage);
        }

        [Fact]
        public void Remove_Should_Remove_Role()
        {
            var doorEntity = DoorAggregate.Create("Door name", null, true, true);

            doorEntity.AddRole(Role.Create("Role"));

            doorEntity.RemoveRole(Role.Create("Role"));

            doorEntity.AllowedRoles.Should().BeEmpty();
        }

        [Theory]
        [InlineData(false, false, "Door is already unlocked.")]
        [InlineData(true, true, "Informed Role is not allowed to unlock this door.")]
        public void Unlock_Should_Fail_To_Unlock_Door(bool isLocked, bool isAccessRestricted, string errorMessage)
        {
            var doorEntity = DoorAggregate.Create("Door name", null, isLocked, isAccessRestricted);
            if (isAccessRestricted)
                doorEntity.AddRole(Role.Create("Restricted Role"));

            var action = () => doorEntity.Unlock(Role.Create("Testing Role"));

            action.Should().Throw<Exception>().WithMessage(errorMessage);
            doorEntity.IsLocked.Should().Be(isLocked);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(true, true)]
        public void Unlock_Should_Unlock_Door(bool isLocked, bool isAccessRestricted)
        {
            var doorEntity = DoorAggregate.Create("Door name", null, isLocked, isAccessRestricted);
            if (isAccessRestricted)
                doorEntity.AddRole(Role.Create("Restricted Role"));

            doorEntity.Unlock(Role.Create("Restricted Role"));

            doorEntity.IsLocked.Should().BeFalse();
        }

        [Fact]
        public void Lock_Should_Fail_To_Lock_Door()
        {
            var doorEntity = DoorAggregate.Create("Door name", null, true, false);

            var action = () => doorEntity.Lock();

            action.Should().Throw<DomainException>().WithMessage("Door is already locked.");
            doorEntity.IsLocked.Should().BeTrue();
        }

        [Fact]
        public void Lock_Should_Lock_Door()
        {
            var doorEntity = DoorAggregate.Create("Door name", null, false, false);

            doorEntity.Lock();

            doorEntity.IsLocked.Should().BeTrue();
        }
    }
}
