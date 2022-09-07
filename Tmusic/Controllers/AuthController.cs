using Application.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Tmusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand loginCommand, CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(loginCommand, cancellationToken);
            return Ok(res);
            
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand loginCommand, CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(loginCommand, cancellationToken);
            return Ok(res);
        }
    }
}
