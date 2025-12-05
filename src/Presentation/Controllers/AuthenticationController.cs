using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Presentation.Models;
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
        [HttpPost("v1/register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("v1/login")]
        public IActionResult Login()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("v1/social")]
        public IActionResult Social()
        {
            return Ok();
        }
    }
}
