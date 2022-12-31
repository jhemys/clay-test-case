using Clay.Application.DTOs;
using Clay.Application.Exceptions;
using Clay.Application.Interfaces.Repositories;
using Clay.Application.Interfaces.Services;
using Clay.Domain.Aggregates.Door;
using Clay.Domain.Aggregates.DoorHistory;
using Clay.Domain.DomainObjects;
using Clay.Domain.ValueObjects;
using Mapster;

namespace Clay.Application.Services
{
    public class DoorService : ApplicationBaseService, IDoorService
    {
        public DoorService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IList<DoorDTO>> GetAll()
        {
            var doors = await UnitOfWork.DoorRepository.GetAll();

            return doors.Adapt<IList<DoorDTO>>();
        }

        public async Task<DoorDTO> GetById(int id)
        {
            var door = await FindById(id);

            return door.Adapt<DoorDTO>();
        }

        public async Task CreateDoor(DoorDTO door)
        {
            var doorToCreate = Door.Create(door.Name, door.Description, door.IsLocked, door.IsAccessRestricted);

            await UnitOfWork.DoorRepository.AddAsync(doorToCreate);

            await UnitOfWork.CommitAsync();
        }

        public async Task UpdateDoor(DoorDTO door)
        {
            var doorToUpdate = await FindById(door.Id);

            doorToUpdate.SetName(door.Name);
            doorToUpdate.SetDescription(door.Description);
            doorToUpdate.SetAccessRestricted(door.IsAccessRestricted);

            UnitOfWork.DoorRepository.Update(doorToUpdate);

            await UnitOfWork.CommitAsync();
        }

        public async Task DeleteDoor(int id)
        {
            var doorToDelete = await FindById(id);

            doorToDelete.SetIsActive(false);

            UnitOfWork.DoorRepository.Update(doorToDelete);

            await UnitOfWork.CommitAsync();
        }

        public async Task AddAllowedRole(int id, RoleDTO roleDto)
        {
            var doorToUpdate = await FindById(id);

            var role = await UnitOfWork.DoorRepository.GetRoleByName(roleDto.Name);

            if (role is null)
            {
                role = Role.Create(roleDto.Name);
                await UnitOfWork.DoorRepository.AddRole(role);
            }

            doorToUpdate.AddRole(role);

            UnitOfWork.DoorRepository.Update(doorToUpdate);

            await UnitOfWork.CommitAsync();
        }

        public async Task RemoveAllowedRole(int id, RoleDTO role)
        {
            var doorToUpdate = await FindById(id);

            doorToUpdate.RemoveRole(role.Adapt<Role>());

            UnitOfWork.DoorRepository.Update(doorToUpdate);

            await UnitOfWork.CommitAsync();
        }

        private async Task<Door> FindById(int id)
        {
            var DoorToUpdate = await UnitOfWork.DoorRepository.GetById(id);

            if (DoorToUpdate is null)
                throw new EntityNotFoundException();

            return DoorToUpdate;
        }

        public async Task UnlockDoor(int doorId, int employeeId, string? tagIdentification)
        {
            var requestDate = DateTime.UtcNow;
            var door = await UnitOfWork.DoorRepository.GetById(doorId);

            if (door is null)
                throw new EntityNotFoundException("Door informed not found.");

            var employee = await UnitOfWork.EmployeeRepository.GetById(employeeId);

            if (employee is null)
                throw new EntityNotFoundException("Employee informed not found.");

            try
            {
                var employeeRole = Role.Create(employee.Role);

                door.Unlock(employeeRole);

                var state = "Unlocked";
                var doorHistory = DoorHistory.Create(doorId, door.Name, employeeId, employee.Name, state, requestDate, tagIdentification);

                await UnitOfWork.DoorHistoryRepository.AddAsync(doorHistory);
                UnitOfWork.DoorRepository.Update(door);
            }
            catch (Exception ex) when (ex is DomainException || ex is DomainActionNotPermittedException)
            {
                var state = "UnlockFailed";
                var doorHistory = DoorHistory.Create(doorId, door.Name, employeeId, employee.Name, state, requestDate, tagIdentification, ex.Message);

                await UnitOfWork.DoorHistoryRepository.AddAsync(doorHistory);

                throw;
            }
            finally
            {
                await UnitOfWork.CommitAsync();
            }
        }

        public async Task LockDoor(int doorId, int employeeId)
        {
            var requestDate = DateTime.UtcNow;
            var door = await UnitOfWork.DoorRepository.GetById(doorId);

            if (door is null)
                throw new EntityNotFoundException("Door informed not found.");

            var employee = await UnitOfWork.EmployeeRepository.GetById(employeeId);

            if (employee is null)
                throw new EntityNotFoundException("Employee informed not found.");

            try
            {
                var employeeRole = Role.Create(employee.Role);

                door.Lock();

                var state = "Locked";
                var doorHistory = DoorHistory.Create(doorId, door.Name, employeeId, employee.Name, state, requestDate, null);

                await UnitOfWork.DoorHistoryRepository.AddAsync(doorHistory);
                UnitOfWork.DoorRepository.Update(door);
            }
            catch (DomainException ex)
            {
                var state = "LockFailed";
                var doorHistory = DoorHistory.Create(doorId, door.Name, employeeId, employee.Name, state, requestDate, null, ex.Message);

                await UnitOfWork.DoorHistoryRepository.AddAsync(doorHistory);

                throw;
            }
            finally
            {
                await UnitOfWork.CommitAsync();
            }
        }
    }
}
