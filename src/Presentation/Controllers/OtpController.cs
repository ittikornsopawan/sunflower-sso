using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using MediatR;

using Presentation.Models;
using Application.OTP.Command;
using Shared.Configurations;
using System.Net;

namespace Presentation.Controllers
{
    [Route("api/otp")]
    [ApiController]
    public class OtpController : BaseController
    {
        private readonly IMediator _mediator;
        public OtpController(IOptions<AppSettings> appSettings, IMediator mediator) : base(appSettings)
        {
            _mediator = mediator;
        }

        [HttpPost("v1/create")]
        public async Task<IActionResult> CreateOTP([FromBody] OtpRequestModel request)
        {
            var command = new CreateOTPCommand(request.purpose, request.contact);
            var result = await _mediator.Send(command);

            if (result.status.statusCode != HttpStatusCode.OK)
                return this.ResponseHandler(result.status.statusCode, result.status.bizErrorCode);

            var response = new OtpResponseModel
            {
                id = result.data.id,
                refCode = result.data.refCode!.value,
                expiresAt = result.data.expiry
            };

            return this.ResponseHandler<OtpResponseModel>(response);
        }

        [HttpPost("v1/verify")]
        public IActionResult VerifyOTP([FromBody] OtpVerifyModel request)
        {
            return Ok();
        }
    }
}
