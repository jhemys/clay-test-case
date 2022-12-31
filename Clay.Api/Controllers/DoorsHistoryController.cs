using Microsoft.AspNetCore.Mvc;

namespace Clay.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class DoorsHistoryController : ControllerBase
    {
        [HttpGet("doors/access-history")]
        public void GetAllAccessHistory()
        {

        }

        [HttpGet("doors/{doorId:int}/access-history")]
        public void GetAllHistory(int doorId)
        {

        }
    }
}
