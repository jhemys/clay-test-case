using Clay.Api.Models;
using Clay.Application.DTOs;
using Clay.Application.Interfaces.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Clay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoorsController : ControllerBase
    {
        private readonly IDoorService _doorService;
        public DoorsController(IDoorService doorService)
        {
            _doorService = doorService;
        }

        [HttpGet]
        [Authorize(Roles = "Employee,FullAccess")]
        public async Task<IList<DoorResponse>> GetAll()
        {
            var doors = await _doorService.GetAll();

            return doors.Adapt<IList<DoorResponse>>();
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Employee,FullAccess")]
        public async Task<DoorResponse> GetById(int id)
        {
            var door = await _doorService.GetById(id);

            return door.Adapt<DoorResponse>();
        }

        [HttpPost]
        [Authorize(Roles = "FullAccess")]
        public async Task<ActionResult> Post([FromBody] DoorRequest request)
        {
            var door = request.Adapt<DoorDTO>();

            await _doorService.CreateDoor(door);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "FullAccess")]
        public async Task<ActionResult> Put(int id, [FromBody] DoorRequest request)
        {
            var door = request.Adapt<DoorDTO>();
            door.Id = id;

            await _doorService.UpdateDoor(door);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "FullAccess")]
        public async Task<ActionResult> Delete(int id)
        {
            await _doorService.DeleteDoor(id);

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}/AddRole")]
        [Authorize(Roles = "FullAccess")]
        public async Task<ActionResult> AddRole(int id, [FromBody] RoleRequest request)
        {
            await _doorService.AddAllowedRole(id, request.Adapt<RoleDTO>());

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}/RemoveRole")]
        [Authorize(Roles = "FullAccess")]
        public async Task<ActionResult> RemoveRole(int id, [FromBody] RoleRequest request)
        {
            await _doorService.RemoveAllowedRole(id, request.Adapt<RoleDTO>());

            return NoContent();
        }

        [HttpPut]
        [Authorize]
        [Route("{id:int}/Unlock")]
        [Authorize(Roles = "Employee,FullAccess")]
        public async Task<ActionResult> Unlock(int id, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] UnlockDoorRequest? request = null)
        {
            var userId = GetUserId();

            int employeeId = 0;
            if (userId is null || !int.TryParse(userId, out employeeId))
                return Unauthorized();

            await _doorService.UnlockDoor(id, employeeId, request?.TagIdentification);

            return NoContent();
        }

        [HttpPut]
        [Authorize]
        [Route("{id:int}/Lock")]
        [Authorize(Roles = "Employee,FullAccess")]
        public async Task<ActionResult> Lock(int id)
        {
            var userId = GetUserId();

            int employeeId = 0;
            if (userId is null || !int.TryParse(userId, out employeeId))
                Unauthorized();

            await _doorService.LockDoor(id, employeeId);

            return NoContent();
        }

        private string? GetUserId()
        {
            return User?.Claims?.FirstOrDefault(x => x.Type == "Id")?.Value;
        }
    }
}
