using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public record OtpCodeValueObject
{
    public string value { get; init; }

    public OtpCodeValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("OTP code cannot be empty.", nameof(value));

        if (!Regex.IsMatch(value, @"^\d{4,6}$"))
            throw new ArgumentException("OTP code must be 4-6 digits.", nameof(value));

        this.value = value;
    }

    public override string ToString() => this.value;
}
