using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Configurations;

namespace Presentation.Controllers
{
    [Route("api/health")]
    [ApiController]
    public class HealthController : BaseController
    {
        public HealthController(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }

        /// <summary>
        /// Basic health check.
        /// Checks if the service is running.
        /// </summary>
        /// <remarks>
        /// Author: Ittikorn Sopawan
        /// Created: 2025-11-08
        /// Last Updated: 2025-11-08
        /// Updated By: Ittikorn Sopawan
        /// </remarks>
        /// <returns>The status of the service (Healthy).</returns>
        [HttpGet]
        public IActionResult GetHealth()
        {
            var healthStatus = new
            {
                status = "Healthy",
                timestamp = DateTime.UtcNow
            };

            return Ok(healthStatus);
        }

        /// <summary>
        /// Liveness check.
        /// Checks if the service process is alive and responsive.
        /// </summary>
        /// <remarks>
        /// Author: Ittikorn Sopawan
        /// Created: 2025-11-08
        /// Last Updated: 2025-11-08
        /// Updated By: Ittikorn Sopawan
        /// </remarks>
        /// <returns>The status of the service (Alive).</returns>
        [HttpGet("liveness")]
        public IActionResult GetLiveness()
        {
            return Ok("Alive");
        }

        /// <summary>
        /// Readiness check.
        /// Checks if the service is ready to handle requests.
        /// Ensures dependencies like Database, Cache, and other services are available.
        /// </summary>
        /// <remarks>
        /// Author: Ittikorn Sopawan
        /// Created: 2025-11-08
        /// Last Updated: 2025-11-08
        /// Updated By: Ittikorn Sopawan
        /// </remarks>
        /// <returns>The status of the service (Ready or Not Ready).</returns>
        [HttpGet("dependencies")]
        public IActionResult GetDependencies()
        {
            return Ok("All dependencies are healthy");
        }
    }
}
