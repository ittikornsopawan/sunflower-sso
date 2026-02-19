using System;
using Application.Otp.Command;
using FluentValidation;

namespace Application.Otp.Validator;

public class CreateOtpCommandValidator : AbstractValidator<CreateOtpCommand>
{
    public CreateOtpCommandValidator()
    {
        RuleFor(x => x.purpose)
            .NotEmpty().WithMessage("Purpose is required.")
            .Must(BeAValidPurpose).WithMessage("Invalid Otp purpose.");

        RuleFor(x => x.contact)
            .NotEmpty().WithMessage("Contact is required.")
            .Must(BeValidEmailOrPhone).WithMessage("Contact must be a valid email or phone number.");
    }

    private bool BeAValidPurpose(string purpose)
    {
        var validPurposes = new[]
        {
            "LOGIN",
            "VERIFY",
            "CONFIRM",
            "RESET_PASSWORD",
            "OTHER"
        };

        return validPurposes.Contains(purpose?.Trim().ToUpper());
    }

    private bool BeValidEmailOrPhone(string contact)
    {
        if (string.IsNullOrWhiteSpace(contact))
            return false;

        if (contact.Contains("@"))
            return EmailIsValid(contact);

        return PhoneIsValid(contact);
    }

    private bool EmailIsValid(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private bool PhoneIsValid(string phone)
    {
        return phone.All(char.IsDigit) && phone.Length == 10;
    }
}
