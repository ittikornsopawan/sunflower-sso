using System;
using Domain.Entities;
using MediatR;
using Shared.Common;

namespace Application.Otp.Command;

public class CreateOtpCommand : IRequest<ResponseModel<OtpEntity>>
{
    /// Purpose of the OTP. Possible values: "LOGIN", "VERIFY", "CONFIRM", "RESET_PASSWORD", "OTHER".
    public string purpose { get; }

    /// Contact information (email or mobile number) to which the OTP will be sent
    public string contact { get; }

    /// <summary>
    /// Initializes a new instance of the CreateOtpCommand class with the specified purpose and contact. This command is used to request the generation of a new OTP (One-Time Password) for a specific purpose (e.g., "login", "reset-password") and contact information (email or mobile number). The command encapsulates the necessary data for creating an OTP and will be handled by the corresponding command handler to perform the OTP generation logic.
    /// </summary>
    /// <param name="purpose">Purpose of the OTP. Possible values: "LOGIN", "VERIFY", "CONFIRM", "RESET_PASSWORD", "OTHER".</param>
    /// <param name="contact">Contact information (email or mobile number) to which the OTP will be sent</param>
    public CreateOtpCommand(string purpose, string contact)
    {
        this.purpose = purpose;
        this.contact = contact;
    }
}
