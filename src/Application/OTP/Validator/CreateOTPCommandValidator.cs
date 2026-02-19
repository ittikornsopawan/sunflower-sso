using System;
using Application.Otp.Command;
using FluentValidation;

namespace Application.Otp.Validator;

public class CreateOtpCommandValidator : AbstractValidator<CreateOtpCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateOtpCommandValidator class, which defines validation rules for the CreateOtpCommand. It ensures that the purpose is not empty and is one of the predefined valid purposes, and that the contact is not empty and is either a valid email address or a valid phone number. The validation rules are implemented using FluentValidation's RuleFor method, with custom validation logic for checking valid purposes and contact formats.
    /// </summary>
    public CreateOtpCommandValidator()
    {
        RuleFor(x => x.purpose)
            .NotEmpty().WithMessage("Purpose is required.")
            .Must(BeAValidPurpose).WithMessage("Invalid Otp purpose.");

        RuleFor(x => x.contact)
            .NotEmpty().WithMessage("Contact is required.")
            .Must(BeValidEmailOrPhone).WithMessage("Contact must be a valid email or phone number.");
    }

    /// <summary>
    /// Checks if the provided purpose is valid by comparing it against a predefined list of valid purposes. The method trims and converts the input to uppercase before checking for validity, ensuring that the validation is case-insensitive and ignores leading or trailing whitespace. Valid purposes include "LOGIN", "VERIFY", "CONFIRM", "RESET_PASSWORD", and "OTHER". If the input matches any of these valid purposes, the method returns true; otherwise, it returns false, indicating an invalid purpose.
    /// </summary>
    /// <param name="purpose">Purpose to validate</param>
    /// <returns>True if the purpose is valid, otherwise false</returns>
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

    /// <summary>
    /// Validates whether the provided contact information is either a valid email address or a valid phone number. The method first checks if the contact string is null or whitespace, returning false if it is. Then, it determines if the contact contains an "@" symbol, which indicates it should be validated as an email address. If it is an email, it uses the System.Net.Mail.MailAddress class to validate the email format. If the contact does not contain an "@", it is treated as a phone number, and the method checks if it consists of exactly 10 digits. The method returns true if the contact is valid as either an email or a phone number, and false otherwise.
    /// </summary>
    /// <param name="contact">Contact information to validate (email or phone number)</param>
    /// <returns>True if the contact is valid, otherwise false</returns>
    private bool BeValidEmailOrPhone(string contact)
    {
        if (string.IsNullOrWhiteSpace(contact))
            return false;

        if (contact.Contains("@"))
            return EmailIsValid(contact);

        return PhoneIsValid(contact);
    }

    /// <summary>
    /// Validates whether the provided email address is in a valid format. The method attempts to create a new instance of the System.Net.Mail.MailAddress class using the input email string. If the instantiation is successful and the address property of the created MailAddress object matches the input email, the method returns true, indicating that the email is valid. If an exception is thrown during instantiation (e.g., due to an invalid email format), the method catches the exception and returns false, indicating that the email is not valid.
    /// </summary>
    /// <param name="email">Email address to validate</param>
    /// <returns>True if the email is valid, otherwise false</returns>
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

    /// <summary>
    /// Validates whether the provided phone number is in a valid format. The method checks if the phone number consists of exactly 10 characters and that all characters are digits. It uses the char.IsDigit method to verify that each character in the phone string is a digit. If the phone number meets both conditions (length of 10 and all characters are digits), the method returns true, indicating that the phone number is valid. If either condition is not met, the method returns false, indicating that the phone number is not valid.
    /// </summary>
    /// <param name="phone">Phone number to validate</param>
    /// <returns>True if the phone number is valid, otherwise false</returns>
    private bool PhoneIsValid(string phone)
    {
        return phone.All(char.IsDigit) && phone.Length == 10;
    }
}
