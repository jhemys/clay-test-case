using Clay.Application.DTOs;

namespace Clay.Application.Interfaces.Services
{
    public interface IDoorService
    {
        Task CreateDoor(DoorDTO Door);
        Task<IList<DoorDTO>> GetAll();
        Task<DoorDTO> GetById(int id);
        Task UpdateDoor(DoorDTO Door);
        Task DeleteDoor(int id);
        Task AddAllowedRole(int id, RoleDTO role);
        Task RemoveAllowedRole(int id, RoleDTO role);
    }
}
