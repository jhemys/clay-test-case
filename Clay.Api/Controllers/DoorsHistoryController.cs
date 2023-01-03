using Clay.Api.Models;
using Clay.Application.Interfaces.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clay.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize(Roles = "FullAccess")]
    public class DoorsHistoryController : ControllerBase
    {
        private readonly IDoorHistoryService _doorHistoryService;
        public DoorsHistoryController(IDoorHistoryService doorHistoryService)
        {
            _doorHistoryService = doorHistoryService;
        }

        [HttpGet("doors/access-history")]
        public async Task<PagedResult<DoorHistoryResponse>> GetAllAccessHistory([FromQuery] int? pageSize, int? page)
        {
            (var pagedResult, var total) = await _doorHistoryService.GetAllPaged(page, pageSize);

            return new PagedResult<DoorHistoryResponse>(pagedResult.Adapt<IList<DoorHistoryResponse>>(), total, page, pageSize);
        }

        [HttpGet("doors/{doorId:int}/access-history")]
        public async Task<PagedResult<DoorHistoryResponse>> GetAllAccessHistoryByDoor(int doorId, [FromQuery] int? pageSize, int? page)
        {
            (var pagedResult, var total) = await _doorHistoryService.GetByDoorIdPaged(doorId, page, pageSize);

            return new PagedResult<DoorHistoryResponse>(pagedResult.Adapt<IList<DoorHistoryResponse>>(), total, page, pageSize);
        }
    }
}
