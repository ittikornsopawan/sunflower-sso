using System;
using System.Net;
using System.Text.Json;
using Application.Common;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.UseCases.Notification.Services;
using Domain.UseCases.Otp.Services;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Common;
using Shared.Configurations;
using Shared.Extensions;

namespace Application.Otp.Command;

public class CreateOtpCommandHandler : CommonHandler, IRequestHandler<CreateOtpCommand, ResponseModel<OtpEntity>>
{
    protected AppSettings _appSettings;
    protected IOtpService _otpService;
    protected INotificationService _notificationService;
    protected IParameterQueryRepository _parameterQueryRepository;
    protected INotificationQueryRepository _notificationQueryRepository;

    public CreateOtpCommandHandler(
        IOptions<AppSettings> appSettings,
        AppDbContext context,
        IOtpService otpService,
        INotificationService notificationService,
        IParameterQueryRepository parameterQueryRepository,
        INotificationQueryRepository notificationQueryRepository) : base(context)
    {
        _appSettings = appSettings.Value;
        _otpService = otpService;
        _notificationService = notificationService;
        _parameterQueryRepository = parameterQueryRepository;
        _notificationQueryRepository = notificationQueryRepository;
    }

    /// <summary>
    /// Handles the creation of an OTP by generating it, retrieving the appropriate notification template, replacing variables in the template with OTP details, and sending the notification to the user. The entire process is wrapped in a database transaction to ensure atomicity. If any step fails, the transaction is rolled back and an appropriate error response is returned. If successful, it returns a response containing the generated OtpEntity.
    /// </summary>
    /// <param name="request">Command Request containing purpose and contact information</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation</param>
    /// <returns></returns>
    public async Task<ResponseModel<OtpEntity>> Handle(CreateOtpCommand request, CancellationToken cancellationToken)
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                var otpResult = await _otpService.GenerateOtp(request.purpose, request.contact);
                if (otpResult == null) throw new Exception("failed to generate otp.");

                var parameters = await _parameterQueryRepository.GetParameters(key: this._appSettings.KeyValue.OtpEmailTemplate);
                if (parameters == null || !parameters.Any()) throw new Exception("parameter is not configured.");

                var parameter = parameters.First();

                var templates = await _notificationQueryRepository.GetNotificationTemplates(key: parameter.value);
                if (templates == null || !templates.Any()) throw new Exception("template is not configured.");

                var template = templates.First();

                var variables = VariableExtension.Parse(template.variables!);
                if (variables == null || !variables.Any()) throw new Exception("template content is invalid.");

                variables["code"] = otpResult.code?.value ?? throw new Exception("OTP code is missing.");
                variables["expiry"] = otpResult.expiry?.ToString("yyyy-MM-dd HH:mm:ss") ?? throw new Exception("OTP expiry is missing.");
                variables["refCode"] = otpResult.refCode?.value.ToString() ?? throw new Exception("OTP refCode is missing.");

                template.content = VariableExtension.Replace(template.content!, variables);

                var isSent = await _notificationService.Send(request.contact, template.content!, template.subject!);

                transaction.Commit();
                return this.SuccessResponse<OtpEntity>(otpResult);
            }
            catch (ArgumentException ex)
            {
                transaction.Rollback();
                Console.WriteLine("Error in CreateOtpCommandHandler: ", ex.Message);
                return this.FailResponse<OtpEntity>(HttpStatusCode.BadRequest, "20001");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("Error in CreateOtpCommandHandler: ", ex.Message);
                return this.FailResponse<OtpEntity>(HttpStatusCode.InternalServerError, "30001");
            }
        }
    }
}