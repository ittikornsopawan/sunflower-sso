using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Configurations;

namespace Presentation.Controllers
{
    [Route("api/policy")]
    [ApiController]
    public class PolicyController : BaseController
    {
        public PolicyController(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }

        [HttpGet("v1/")]
        public IActionResult GetPolicy()
        {
            return Ok();
        }

        [HttpPost("v1/")]
        public IActionResult CreatePolicies()
        {
            return Ok();
        }

        [HttpPut("v1/{id}")]
        public IActionResult UpdatePolicy()
        {
            return Ok();
        }

        [HttpDelete("v1/{id}")]
        public IActionResult DeletePolicy()
        {
            return Ok();
        }

        [HttpGet("v1/{id}/versions")]
        public IActionResult GetPolicyVersion()
        {
            return Ok();
        }

        [HttpPost("v1/{id}/versions")]
        public IActionResult CreatePolicyVersion()
        {
            return Ok();
        }
    }
}
