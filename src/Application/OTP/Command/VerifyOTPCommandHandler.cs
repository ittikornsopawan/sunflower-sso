using System;
using System.Net;
using Application.Common;
using Domain.Entities;
using Domain.UseCases.Otp.Services;
using Infrastructure.Persistence;
using MediatR;
using Shared.Common;

namespace Application.Otp.Command;

public class VerifyOtpCommandHandler : CommonHandler, IRequestHandler<VerifyOtpCommand, ResponseModel<OtpEntity>>
{
    protected IOtpService _otpService;

    public VerifyOtpCommandHandler(AppDbContext dbContext, IOtpService otpService) : base(dbContext)
    {
        _otpService = otpService;
    }

    public async Task<ResponseModel<OtpEntity>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var otpEntity = await _otpService.Verify(code: request.code, refCode: request.refCode);
            return this.SuccessResponse<OtpEntity>(otpEntity!, HttpStatusCode.OK);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error verifying OTP: {ex.Message}");
            return this.FailMessageResponse<OtpEntity>(HttpStatusCode.BadRequest, "20000");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error verifying OTP: {ex.Message}");
            return this.FailMessageResponse<OtpEntity>(HttpStatusCode.InternalServerError, "30000");
        }
    }
}
