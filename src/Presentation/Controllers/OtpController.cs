using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using MediatR;

using Presentation.Models;
using Application.Otp.Command;
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
            var command = new CreateOtpCommand(request.purpose, request.contact);
            var result = await _mediator.Send(command);

            if (result.status.statusCode != HttpStatusCode.OK)
                return this.ResponseHandler(result.status.statusCode, result.status.bizErrorCode);

            var response = new OtpResponseModel
            {
                refCode = result.data.refCode!.value,
                expiresAt = result.data.expiry
            };

            return this.ResponseHandler<OtpResponseModel>(response);
        }

        [HttpPost("v1/verify")]
        public async Task<IActionResult> VerifyOTP([FromBody] OtpVerifyRequestModel request)
        {
            var command = new VerifyOTPCommand(request.code, request.refCode);
            var result = await _mediator.Send(command);

            if (result.status.statusCode != HttpStatusCode.OK)
                return this.ResponseHandler(result.status.statusCode, result.status.bizErrorCode);

            var response = new OtpVerifyResponseModel
            {
                id = Guid.NewGuid()
            };

            return this.ResponseHandler<OtpVerifyResponseModel>(response);
        }
    }
}
