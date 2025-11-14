using System;
using System.Text;

namespace Domain.ValueObjects;

public record UsernameValueObject
{
    private readonly byte[] _valueBytes;

    public string Value => Encoding.UTF8.GetString(_valueBytes);

    public UsernameValueObject(byte[] value)
    {
        if (value == null || value.Length == 0)
            throw new ArgumentException("Username cannot be empty.", nameof(value));

        if (value.Length < 3)
            throw new ArgumentException("Username must be at least 3 bytes.", nameof(value));

        _valueBytes = value;
    }

    public byte[] ToBytes() => _valueBytes;

    public override string ToString() => Value;
}