using System;
using Domain.Entities;
using MediatR;
using Shared.Common;

namespace Application.Otp.Command;

public class CreateOtpCommand : IRequest<ResponseModel<OtpEntity>>
{
    public string purpose { get; }
    public string contact { get; }
    public CreateOtpCommand(string purpose, string contact)
    {
        this.purpose = purpose;
        this.contact = contact;
    }
}
