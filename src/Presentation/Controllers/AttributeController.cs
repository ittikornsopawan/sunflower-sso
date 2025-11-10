using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Configurations;

namespace Presentation.Controllers
{
    [Route("api/attribute")]
    [ApiController]
    public class AttributeController : BaseController
    {
        public AttributeController(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }

        [HttpGet("v1")]
        public IActionResult GetAttributes()
        {
            return Ok();
        }

        [HttpPost("v1")]
        public IActionResult CreateAttributes()
        {
            return Ok();
        }

        [HttpPut("v1/{id}")]
        public IActionResult UpdateAttributes()
        {
            return Ok();
        }

        [HttpDelete("v1/{id}")]
        public IActionResult DeleteAttributes()
        {
            return Ok();
        }

        [HttpGet("v1/group")]
        public IActionResult GetAttributeGroups()
        {
            return Ok();
        }

        [HttpPost("v1/group/{id}")]
        public IActionResult CreateAttributeGroups()
        {
            return Ok();
        }

        [HttpPut("v1/group/{id}")]
        public IActionResult UpdateAttributeGroups()
        {
            return Ok();
        }

        [HttpDelete("v1/group/{id}")]
        public IActionResult DeleteAttributeGroups()
        {
            return Ok();
        }
    }
}
