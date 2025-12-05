using System;

namespace Domain.ValueObjects;

public sealed record MessageValueObject
{
    public byte[] value { get; init; }

    public MessageValueObject(byte[] value)
    {
        if (value == null || value.Length == 0)
            throw new ArgumentException("Message cannot be empty.", nameof(value));

        this.value = value;
    }
}
