using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost("v1/login")]
        public IActionResult Login()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("v1/login/mobile")]
        public IActionResult LoginMobile()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("v1/login/email")]
        public IActionResult LoginEmail()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("v1/login/social")]
        public IActionResult LoginSocial()
        {
            return Ok();
        }
    }
}
