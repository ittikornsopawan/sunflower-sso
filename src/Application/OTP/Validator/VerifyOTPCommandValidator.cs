using System;
using Application.Otp.Command;
using FluentValidation;

namespace Application.Otp.Validator;

public class VerifyOTPCommandValidator : AbstractValidator<VerifyOTPCommand>
{
    public VerifyOTPCommandValidator()
    {
        // RuleFor(x => x.id)
        //     .NotEmpty()
        //     .WithMessage("Id is required.");

        RuleFor(x => x.code)
            .NotEmpty().WithMessage("OTP code is required.")
            .Length(4, 8).WithMessage("OTP code must be between 4 and 8 characters.");

        RuleFor(x => x.refCode)
            .NotEmpty().WithMessage("Reference code is required.")
            .MaximumLength(8).WithMessage("Reference code is too long.");
    }
}
