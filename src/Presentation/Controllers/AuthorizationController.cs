using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Configurations;

namespace Presentation.Controllers
{
    [Route("api/authorization")]
    [ApiController]
    public class AuthorizationController : BaseController
    {
        public AuthorizationController(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }

        [HttpPost("v1/abac")]
        public IActionResult GetABAC()
        {
            return Ok();
        }

        [HttpPost("v1/policy")]
        public IActionResult GetABACPolicy()
        {
            return Ok();
        }
    }
}
