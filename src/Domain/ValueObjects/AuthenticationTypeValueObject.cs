using System;

namespace Domain.ValueObjects;

public sealed record AuthenticationTypeValueObject
{
    public string value { get; }

    private static readonly HashSet<string> _allowed =
    [
        "PASSWORD",
        "OAUTH",
        "EMAIL_OTP",
        "MOBILE_OTP"
    ];

    public AuthenticationTypeValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Authentication type cannot be empty.", nameof(value));

        var normalized = value.Trim().ToUpperInvariant();

        if (!_allowed.Contains(normalized))
            throw new ArgumentException(
                $"Invalid authentication type '{value}'. Allowed types: {string.Join(", ", _allowed)}",
                nameof(value));

        this.value = normalized;
    }

    public override string ToString() => this.value;

    public static AuthenticationTypeValueObject Password() => new("PASSWORD");
    public static AuthenticationTypeValueObject OAuth() => new("OAUTH");
    public static AuthenticationTypeValueObject EmailOtp() => new("EMAIL_OTP");
    public static AuthenticationTypeValueObject MobileOtp() => new("MOBILE_OTP");
}