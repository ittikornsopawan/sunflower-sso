using System;
using Domain.Entities;
using MediatR;
using Shared.Common;

namespace Application.OTP.Command;

public class CreateOTPCommand : IRequest<ResponseModel<OTPEntity>>
{
    public string purpose { get; }
    public string contact { get; }
    public CreateOTPCommand(string purpose, string contact)
    {
        this.purpose = purpose;
        this.contact = contact;
    }
}
