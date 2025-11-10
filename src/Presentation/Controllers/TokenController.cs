using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Configurations;

namespace Presentation.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : BaseController
    {
        public TokenController(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }

        [HttpPost("introspect")]
        public IActionResult Introspect()
        {
            return Ok();
        }

        [HttpPost("revoke")]
        public IActionResult Revoke()
        {
            return Ok();
        }
    }
}
