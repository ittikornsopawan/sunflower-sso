using System;

namespace Domain.ValueObjects;

public record PasswordHashValueObject
{
    public string value { get; init; }

    public PasswordHashValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password hash cannot be empty.", nameof(value));

        this.value = value;
    }

    public override string ToString() => this.value;
}
