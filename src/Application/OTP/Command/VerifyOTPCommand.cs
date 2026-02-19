using System;
using Domain.Entities;
using MediatR;
using Shared.Common;

namespace Application.Otp.Command;

public class VerifyOtpCommand : IRequest<ResponseModel<OtpEntity>>
{
    public string code { get; }
    public string refCode { get; }

    public VerifyOtpCommand(string code, string refCode)
    {
        this.code = code;
        this.refCode = refCode;
    }
}
