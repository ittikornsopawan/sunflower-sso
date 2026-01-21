using System;
using MediatR;
using Shared.Common;

namespace Application.Otp.Command;

public class VerifyOTPCommand : IRequest<ResponseModel<Guid>>
{
    public string code { get; }
    public string refCode { get; }

    public VerifyOTPCommand(string code, string refCode)
    {
        this.code = code;
        this.refCode = refCode;
    }
}
