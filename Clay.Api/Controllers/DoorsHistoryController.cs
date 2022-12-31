using Clay.Api.Models;
using Clay.Application.Interfaces.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Clay.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class DoorsHistoryController : ControllerBase
    {
        private readonly IDoorHistoryService _doorHistoryService;
        public DoorsHistoryController(IDoorHistoryService doorHistoryService)
        {
            _doorHistoryService = doorHistoryService;
        }

        [HttpGet("doors/access-history")]
        public async Task<ActionResult<PagedResult<DoorHistoryResponse>>> GetAllAccessHistory([FromQuery] int? pageSize, int? page)
        {
            (var pagedResult, var total) = await _doorHistoryService.GetAllPaged(page, pageSize);

            return Ok(new PagedResult<DoorHistoryResponse>(pagedResult.Adapt<IList<DoorHistoryResponse>>(), total, page, pageSize));
        }

        [HttpGet("doors/{doorId:int}/access-history")]
        public async Task<ActionResult<PagedResult<DoorHistoryResponse>>> GetAllHistory(int doorId, [FromQuery] int? pageSize, int? page)
        {
            (var pagedResult, var total) = await _doorHistoryService.GetByDoorIdPaged(doorId, page, pageSize);

            return Ok(new PagedResult<DoorHistoryResponse>(pagedResult.Adapt<IList<DoorHistoryResponse>>(), total, page, pageSize));
        }
    }
}
