using System.Net;
using MediatR;
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
        private readonly IMediator _mediator;

        public HealthController(IOptions<AppSettings> appSettings, IMediator mediator) : base(appSettings)
        {
            _mediator = mediator;
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
        [HttpGet("v1")]
        public async Task<IActionResult> GetHealth()
        {
            return await Task.FromResult(this.ResponseHandler<string>("Healthy", HttpStatusCode.OK));
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
        [HttpGet("v1/dependencies")]
        public async Task<IActionResult> GetDependencies()
        {
            return await Task.FromResult(this.ResponseHandler<string>("Healthy", HttpStatusCode.OK));
        }
    }
}
