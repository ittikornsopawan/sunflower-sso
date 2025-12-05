using System;
using System.Net;
using Application.Common;
using Domain.Entities;
using Domain.UseCases.Notification.Services;
using Domain.UseCases.Otp.Services;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Common;
using Shared.Configurations;

namespace Application.OTP.Command;

public class CreateOTPCommandHandler : CommonHandler, IRequestHandler<CreateOTPCommand, ResponseModel<OTPEntity>>
{
    protected AppSettings _appSettings;
    protected IOtpService _otpService;
    protected INotificationService _notificationService;

    public CreateOTPCommandHandler(IOptions<AppSettings> appSettings, AppDbContext context, IOtpService otpService, INotificationService notificationService) : base(context)
    {
        _appSettings = appSettings.Value;
        _otpService = otpService;
        _notificationService = notificationService;
    }

    public async Task<ResponseModel<OTPEntity>> Handle(CreateOTPCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var otpResult = await _otpService.GenerateOTP(request.purpose, request.contact);
            return this.SuccessResponse<OTPEntity>(otpResult);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error in CreateOTPCommandHandler: ", ex.Message);
            return this.FailResponse<OTPEntity>(HttpStatusCode.BadRequest, "20001");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in CreateOTPCommandHandler: ", ex.Message);
            return this.FailResponse<OTPEntity>(HttpStatusCode.InternalServerError, "30001");
        }

    }
}