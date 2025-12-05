using System;

namespace Domain.ValueObjects;

/// <summary>
/// A general-purpose Key/Value pair Value Object.
/// Immutable and self-validated.
/// </summary>
public sealed record KeyValueObject
{
    public string value { get; init; }

    public KeyValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Key cannot be empty.", nameof(value));

        this.value = value;
    }
}