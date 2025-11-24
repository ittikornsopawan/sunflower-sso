using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public sealed record MobileNumberValueObject
{
    public string value { get; init; }

    public MobileNumberValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Mobile number cannot be empty.", nameof(value));

        if (!Regex.IsMatch(value, @"^\+\d{9,15}$"))
            throw new ArgumentException("Mobile number format is invalid.", nameof(value));

        this.value = value;
    }

    public override string ToString() => this.value;
}
