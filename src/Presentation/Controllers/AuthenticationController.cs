using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Presentation.Models.Authentication;
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
        public IActionResult Register([FromBody] RegisterModel request)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("v1/register/mobile")]
        public IActionResult RegisterMobile()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("v1/register/email")]
        public IActionResult RegisterEmail()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("v1/register/social")]
        public IActionResult RegisterSocial()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("v1/login")]
        public IActionResult Login([FromBody] LoginModel request)
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
