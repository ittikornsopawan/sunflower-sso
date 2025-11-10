using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Configurations;

namespace Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPost("login/mobile")]
        public IActionResult LoginMobile()
        {
            return Ok();
        }

        [HttpPost("login/email")]
        public IActionResult LoginEmail()
        {
            return Ok();
        }

        [HttpPost("login/social")]
        public IActionResult LoginSocial()
        {
            return Ok();
        }
    }
}
