using System;
using Application.Authentication.Command;
using FluentValidation;
using Shared.DTOs;

namespace Application.Authentication.Validator;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        // Validate UserDTO
        RuleFor(x => x.user)
            .NotNull().WithMessage("User information is required.");

        // Validate UserAuthenticationDTO
        RuleFor(x => x.userAuthentication)
            .NotNull().WithMessage("User authentication information is required.");

        // Optionally validate addresses if provided
        When(x => x.userAddresses != null && x.userAddresses.Count > 0, () =>
        {
            RuleForEach(x => x.userAddresses)
                .SetValidator(new AddressValidator());
        });

        // Optionally validate contacts if provided
        When(x => x.userContact != null && x.userContact.Count > 0, () =>
        {
            RuleForEach(x => x.userContact)
                .SetValidator(new ContactValidator());
        });
    }

    // Nested validator for AddressDTO
    private class AddressValidator : AbstractValidator<AddressDTO>
    {
        public AddressValidator()
        {
            RuleFor(x => x.type)
                .NotEmpty().WithMessage("Address type is required.");

            RuleFor(x => x.address)
                .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.postalCode)
                .Matches(@"^\d{5}$")
                .When(x => !string.IsNullOrWhiteSpace(x.postalCode))
                .WithMessage("Postal code must be 5 digits.");
        }
    }

    // Nested validator for ContactDTO
    private class ContactValidator : AbstractValidator<ContactDTO>
    {
        public ContactValidator()
        {
            RuleFor(x => x.channel)
                .NotEmpty().WithMessage("Contact channel is required.");

            RuleFor(x => x.contact)
                .NotEmpty().WithMessage("Contact is required.");

            RuleFor(x => x.contactName)
                .NotEmpty().WithMessage("Contact name is required.");
        }
    }
}