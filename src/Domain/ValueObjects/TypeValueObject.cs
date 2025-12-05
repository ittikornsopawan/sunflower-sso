using System;

namespace Domain.ValueObjects;

public sealed record TypeValueObject
{
    public string value { get; init; }

    private static readonly HashSet<string> AllowedTypes = new()
    {
        "EMAIL", "SMS", "PUSH"
    };

    public TypeValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Notification type cannot be empty.", nameof(value));

        value = value.Trim().ToUpper();

        if (!AllowedTypes.Contains(value))
            throw new ArgumentException($"Invalid notification type '{value}'. Allowed: EMAIL, SMS, PUSH.", nameof(value));

        this.value = value;
    }
}
