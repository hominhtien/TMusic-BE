using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Tmusic.Controllers
{
    [Route("api/black-list")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("sync-stm")]
        public async Task<IActionResult> SyncStmLocation( CancellationToken cancellationToken)
        {
            return Ok("OK");
        }

    }
}
