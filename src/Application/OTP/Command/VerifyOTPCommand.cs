using System;
using Domain.Entities;
using MediatR;
using Shared.Common;

namespace Application.Otp.Command;

public class VerifyOtpCommand : IRequest<ResponseModel<OtpEntity>>
{
    public Guid id { get; }
    public string code { get; }
    public string refCode { get; }

    public VerifyOtpCommand(Guid id, string code, string refCode)
    {
        this.id = id;
        this.code = code;
        this.refCode = refCode;
    }
}
