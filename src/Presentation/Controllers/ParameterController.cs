using MediatR;
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
        private readonly IMediator _mediator;
        public ParameterController(IOptions<AppSettings> appSettings, IMediator mediator) : base(appSettings)
        {
            _mediator = mediator;
        }
    }
}
