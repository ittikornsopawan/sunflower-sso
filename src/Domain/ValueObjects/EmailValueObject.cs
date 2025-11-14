using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public record EmailValueObject
{
    public string value { get; init; }

    public EmailValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty.", nameof(value));

        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Email format is invalid.", nameof(value));

        this.value = value;
    }

    public override string ToString() => this.value;
}