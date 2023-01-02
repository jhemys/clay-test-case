using Clay.Application.DTOs;
using Clay.Application.Exceptions;
using Clay.Application.Interfaces.Repositories;
using Clay.Application.Services;
using Clay.Domain.Aggregates.Door;
using Clay.Domain.Aggregates.DoorHistory;
using Clay.Domain.Aggregates.Employee;
using Clay.Domain.DomainObjects;
using Clay.Domain.DomainObjects.Exceptions;

namespace Clay.Tests.Application.Services
{
    public class DoorServiceTests
    {
        [Fact]
        public async Task GetAll_Should_Return_Data()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var doors = PrepareDoorList();
            A.CallTo(() => unitOfWork.DoorRepository.GetAll()).Returns(doors);

            var data = await service.GetAll();

            data.Count.Should().Be(1);
        }

        [Fact]
        public async Task GetById_Should_Return_Data()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var door = PrepareDoorData();
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(door);

            var data = await service.GetById(1);

            data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_Should_Fail_With_Invalid_Entry()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var entity = PrepareDoorData(true);
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(entity);

            var action = () => service.GetById(1);

            await action.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Entry not found.");
        }

        [Fact]
        public async Task CreateDoor_Should_Fail_With_Invalid_Entry()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var entity = PrepareDoorData(true);
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(entity);
            var door = new DoorDTO()
            {
                Description = "Test",
                IsLocked = true,
                IsAccessRestricted = true,
            };

            var action = async() => await service.CreateDoor(door);

            await action.Should().ThrowAsync<DomainException>().WithMessage("The parameter Name is required.");
            A.CallTo(() => unitOfWork.DoorRepository.AddAsync(A<Door>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task CreateDoor_Should_Create_Door()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var door = new DoorDTO()
            {
                Name = "Test",
                Description = "Test",
                IsLocked = true,
                IsAccessRestricted = true,
            };

            await service.CreateDoor(door);

            A.CallTo(() => unitOfWork.DoorRepository.AddAsync(A<Door>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateDoor_Should_Fail_With_Invalid_Data()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var door = PrepareDoorData();
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(door);
            var doorDto = new DoorDTO()
            {
                Description = "Test",
                IsLocked = true,
                IsAccessRestricted = true,
            };

            var action = async () => await service.UpdateDoor(doorDto);

            await action.Should().ThrowAsync<DomainException>().WithMessage("The parameter Name is required.");
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateDoor_Should_Fail_With_Invalid_Entry()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var entity = PrepareDoorData(true);
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(entity);
            var doorDto = new DoorDTO()
            {
                Description = "Test",
                IsLocked = true,
                IsAccessRestricted = true,
            };

            var action = async () => await service.UpdateDoor(doorDto);

            await action.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Entry not found.");
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateDoor_Should_Update_Door()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var door = PrepareDoorData();
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(door);
            var doorDto = new DoorDTO()
            {
                Name = "Test",
                Description = "Test",
                IsLocked = true,
                IsAccessRestricted = true,
            };

            await service.UpdateDoor(doorDto);

            door.Name.Should().Be("Test");
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteDoor_Should_Fail_With_Invalid_Entry()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var entity = PrepareDoorData(true);
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(entity);

            var action = async () => await service.DeleteDoor(1);

            await action.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Entry not found.");
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task DeleteDoor_Should_Update_Door()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var door = PrepareDoorData();
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(door);

            await service.DeleteDoor(1);

            door.IsActive.Should().BeFalse();
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddAllowedRole_Should_Fail_With_Invalid_Entry()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var doorEntity = PrepareDoorData(true);
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(doorEntity);
            var roleDto = new RoleDTO
            {
                Name = "Test",
            };

            var action = async () => await service.AddAllowedRole(1, roleDto);

            await action.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Entry not found.");
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task AddAllowedRole_NewRole_Should_Fail_With_Invalid_Data()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var doorEntity = PrepareDoorData();
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(doorEntity);
            A.CallTo(() => unitOfWork.DoorRepository.GetRoleByName(A<string>._)).Returns((Role?)null);
            var roleDto = new RoleDTO();

            var action = async () => await service.AddAllowedRole(1, roleDto);

            await action.Should().ThrowAsync<DomainException>().WithMessage("The parameter Name is required.");
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task AddAllowedRole_New_Role_Should_Create_New_Role_And_Add_To_Door()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var doorEntity = PrepareDoorData();
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(doorEntity);
            A.CallTo(() => unitOfWork.DoorRepository.GetRoleByName(A<string>._)).Returns((Role?)null);
            var roleDto = new RoleDTO()
            {
                Name = "Test"
            };

            await service.AddAllowedRole(1, roleDto);

            doorEntity.AllowedRoles.Should().OnlyContain(x => x.Name == "Test");
            A.CallTo(() => unitOfWork.DoorRepository.AddRole(A<Role>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddAllowedRole_Existing_Role_Should_Add_Role_To_Door()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var doorEntity = PrepareDoorData();
            var roleEntity = Role.Create("Existing Role");
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(doorEntity);
            A.CallTo(() => unitOfWork.DoorRepository.GetRoleByName(A<string>._)).Returns(roleEntity);
            var roleDto = new RoleDTO()
            {
                Name = "Existing Role"
            };

            await service.AddAllowedRole(1, roleDto);

            doorEntity.AllowedRoles.Should().OnlyContain(x => x.Name == "Existing Role");
            A.CallTo(() => unitOfWork.DoorRepository.AddRole(A<Role>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RemoveAllowedRole_Should_Fail_With_Invalid_Entry()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var doorEntity = PrepareDoorData(true);
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(doorEntity);
            var roleDto = new RoleDTO
            {
                Name = "Test",
            };

            var action = async () => await service.RemoveAllowedRole(1, roleDto);

            await action.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Entry not found.");
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task RemoveAllowedRole_NewRole_Should_Fail_With_Invalid_Data()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var doorEntity = PrepareDoorData();
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(doorEntity);
            A.CallTo(() => unitOfWork.DoorRepository.GetRoleByName(A<string>._)).Returns((Role?)null);
            var roleDto = new RoleDTO()
            {
                Name = "Test",
            };

            var action = async () => await service.RemoveAllowedRole(1, roleDto);

            await action.Should().ThrowAsync<DomainException>().WithMessage("Informed role does not exist for this door.");
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task RemoveAllowedRole_Should_Remove_Role_From_Door()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var doorEntity = PrepareDoorData();
            var roleEntity = Role.Create("Existing Role");
            doorEntity.AddRole(roleEntity);
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(doorEntity);
            var roleDto = new RoleDTO()
            {
                Name = "Existing Role"
            };

            await service.RemoveAllowedRole(1, roleDto);

            doorEntity.AllowedRoles.Should().BeEmpty();
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineData(true, false, "Door informed not found.")]
        [InlineData(false, true, "Employee informed not found.")]
        public async Task UnlockDoor_Should_Fail_With_Invalid_Entry(bool createNullDoorEntity, bool createNullEmployeeEntity, string errorMessage)
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var doorEntity = PrepareDoorData(createNullDoorEntity);
            var employeeEntity = PrepareEmployeeData(createNullEmployeeEntity);
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(doorEntity);
            A.CallTo(() => unitOfWork.EmployeeRepository.GetById(A<int>._)).Returns(employeeEntity);

            var action = async () => await service.UnlockDoor(1, 1, null);

            await action.Should().ThrowAsync<EntityNotFoundException>().WithMessage(errorMessage);
            A.CallTo(() => unitOfWork.DoorHistoryRepository.AddAsync(A<DoorHistory>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task UnlockDoor_Should_Fail_With_Invalid_Tentative_And_Log_Error()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var doorEntity = PrepareDoorData();
            doorEntity.AddRole(Role.Create("Role 2"));
            var employeeEntity = PrepareEmployeeData();
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(doorEntity);
            A.CallTo(() => unitOfWork.EmployeeRepository.GetById(A<int>._)).Returns(employeeEntity);

            var action = async () => await service.UnlockDoor(1, 1, null);

            await action.Should().ThrowAsync<DomainActionNotPermittedException>().WithMessage("Informed Role is not allowed to unlock this door.");
            A.CallTo(() => unitOfWork.DoorHistoryRepository.AddAsync(A<DoorHistory>.Ignored)).MustHaveHappened();
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustHaveHappened();
        }

        [Fact]
        public async Task UnlockDoor_Should_Unlock_Door()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var doorEntity = PrepareDoorData();
            doorEntity.AddRole(Role.Create("Role"));
            var employeeEntity = PrepareEmployeeData();
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(doorEntity);
            A.CallTo(() => unitOfWork.EmployeeRepository.GetById(A<int>._)).Returns(employeeEntity);

            await service.UnlockDoor(1, 1, null);

            doorEntity.IsLocked.Should().BeFalse();
            A.CallTo(() => unitOfWork.DoorHistoryRepository.AddAsync(A<DoorHistory>.Ignored)).MustHaveHappened();
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustHaveHappened();
        }

        [Theory]
        [InlineData(true, false, "Door informed not found.")]
        [InlineData(false, true, "Employee informed not found.")]
        public async Task LockDoor_Should_Fail_With_Invalid_Entry(bool createNullDoorEntity, bool createNullEmployeeEntity, string errorMessage)
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var doorEntity = PrepareDoorData(createNullDoorEntity);
            var employeeEntity = PrepareEmployeeData(createNullEmployeeEntity);
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(doorEntity);
            A.CallTo(() => unitOfWork.EmployeeRepository.GetById(A<int>._)).Returns(employeeEntity);

            var action = async () => await service.LockDoor(1, 1);

            await action.Should().ThrowAsync<EntityNotFoundException>().WithMessage(errorMessage);
            A.CallTo(() => unitOfWork.DoorHistoryRepository.AddAsync(A<DoorHistory>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task LockDoor_Should_Fail_With_Invalid_Tentative_And_Log_Error()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var doorEntity = PrepareDoorData();
            var roleEntity = Role.Create("Role");
            var employeeEntity = PrepareEmployeeData();
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(doorEntity);
            A.CallTo(() => unitOfWork.EmployeeRepository.GetById(A<int>._)).Returns(employeeEntity);

            var action = async () => await service.LockDoor(1, 1);

            await action.Should().ThrowAsync<DomainException>().WithMessage("Door is already locked.");
            A.CallTo(() => unitOfWork.DoorHistoryRepository.AddAsync(A<DoorHistory>.Ignored)).MustHaveHappened();
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustHaveHappened();
        }

        [Fact]
        public async Task LockDoor_Should_Lock_Door()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new DoorService(unitOfWork);
            var doorEntity = PrepareDoorData();
            var roleEntity = Role.Create("Role");
            doorEntity.AddRole(roleEntity);
            doorEntity.Unlock(roleEntity);
            var employeeEntity = PrepareEmployeeData();
            A.CallTo(() => unitOfWork.DoorRepository.GetById(A<int>._)).Returns(doorEntity);
            A.CallTo(() => unitOfWork.EmployeeRepository.GetById(A<int>._)).Returns(employeeEntity);

            await service.LockDoor(1, 1);

            doorEntity.IsLocked.Should().BeTrue();
            A.CallTo(() => unitOfWork.DoorHistoryRepository.AddAsync(A<DoorHistory>.Ignored)).MustHaveHappened();
            A.CallTo(() => unitOfWork.DoorRepository.Update(A<Door>.Ignored)).MustHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustHaveHappened();
        }

        private List<Door> PrepareDoorList()
        {
            return new List<Door>()
            {
                Door.Create("Door", "Description", true, false)
            };
        }

        private Door? PrepareDoorData(bool createNullObject = false)
        {
            if (createNullObject)
                return null;

            return Door.Create("Door", "Description", true, true);
        }

        private Employee? PrepareEmployeeData(bool createNullObject = false)
        {
            if (createNullObject)
                return null;

            return Employee.Create("Name", "Role");
        }
    }
}
