using System;
using Application.Otp.Command;
using FluentValidation;

namespace Application.Otp.Validator;

public class VerifyOtpCommandValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpCommandValidator()
    {
        RuleFor(x => x.code)
            .NotEmpty().WithMessage("Otp code is required.")
            .Length(4, 8).WithMessage("Otp code must be between 4 and 8 characters.");

        RuleFor(x => x.refCode)
            .NotEmpty().WithMessage("Reference code is required.")
            .MaximumLength(8).WithMessage("Reference code is too long.");
    }
}
