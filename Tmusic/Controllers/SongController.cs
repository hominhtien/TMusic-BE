using Microsoft.AspNetCore.Mvc;

namespace Tmusic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class SongController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> DeletePricingAutoSettingByRoute()
        {
            return Ok("123123213213211232");
        }

    }
}
