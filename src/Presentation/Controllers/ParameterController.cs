using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Configurations;

namespace Presentation.Controllers
{
    [Route("api/parameter")]
    [ApiController]
    public class ParameterController : BaseController
    {
        public ParameterController(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }

        [HttpGet("v1")]
        public IActionResult GetParameters([FromBody] List<string> keys)
        {
            return Ok();
        }

        [HttpPost("v1")]
        public IActionResult CreateParameters([FromBody] Dictionary<string, string> parameters)
        {
            return Ok();
        }

        [HttpDelete("v1")]
        public IActionResult DeleteParameters([FromBody] List<string> keys)
        {
            return Ok();
        }
    }
}
