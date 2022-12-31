using Clay.Api.Models;
using Clay.Application.DTOs;
using Clay.Application.Interfaces.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IList<DoorResponse>> GetAll()
        {
            var doors = await _doorService.GetAll();

            return doors.Adapt<IList<DoorResponse>>();
        }

        [HttpGet("{id:int}")]
        public async Task<DoorResponse> GetById(int id)
        {
            var door = await _doorService.GetById(id);

            return door.Adapt<DoorResponse>();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DoorRequest request)
        {
            var door = request.Adapt<DoorDTO>();

            await _doorService.CreateDoor(door);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DoorRequest request)
        {
            var door = request.Adapt<DoorDTO>();
            door.Id = id;

            await _doorService.UpdateDoor(door);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _doorService.DeleteDoor(id);

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}/AddRole")]
        public async Task<ActionResult> AddRole(int id, [FromBody] RoleRequest request)
        {
            await _doorService.AddAllowedRole(id, request.Adapt<RoleDTO>());

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}/RemoveRole")]
        public async Task<ActionResult> RemoveRole(int id, [FromBody] RoleRequest request)
        {
            await _doorService.RemoveAllowedRole(id, request.Adapt<RoleDTO>());

            return NoContent();
        }
    }
}
