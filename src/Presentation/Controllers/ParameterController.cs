using System.Net;
using Application.Parameter.Query;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Configurations;
using Shared.DTOs;

namespace Presentation.Controllers
{
    [Route("api/parameter")]
    [ApiController]
    public class ParameterController : BaseController
    {
        private readonly IMediator _mediator;
        public ParameterController(IOptions<AppSettings> appSettings, IMediator mediator) : base(appSettings)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a list of parameters filtered by key and/or category.
        /// Only parameters that are currently effective and not expired are returned.
        /// </summary>
        /// <param name="key">
        /// Optional query parameter. The key of the parameter to filter by.
        /// If not provided, all keys in the specified category (if any) will be returned.
        /// </param>
        /// <param name="category">
        /// Optional query parameter. The category of parameters to filter by.
        /// If not provided, all categories will be considered.
        /// </param>
        /// <param name="language">
        /// Optional query parameter. The language of parameters to filter by.
        /// If not provided, all language will be considered.
        /// </param>
        /// <returns>
        /// Returns a list of parameters (<see cref="ParameterDTO"/>) matching the given key and/or category.
        /// Returns 404 if no parameters are found.
        /// </returns>
        [HttpGet("v1")]
        public async Task<IActionResult> GetParameters([FromQuery] string? key, [FromQuery] string? category, [FromQuery] string? language)
        {
            var command = new ParameterQuery(key, category, language);
            var response = await _mediator.Send(command);

            if (response.status.statusCode != HttpStatusCode.OK)
                return this.ResponseHandler(response.status.statusCode, response.status.bizErrorCode);

            if (response.data == null || !response.data.Any())
                return this.ResponseHandler(HttpStatusCode.NotFound);

            return this.ResponseHandler<List<ParameterEntity>>(response.data!, HttpStatusCode.OK);
        }
    }
}
