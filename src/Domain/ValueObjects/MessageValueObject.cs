using System;
using System.Text;

namespace Domain.ValueObjects;

public sealed record MessageValueObject
{
    public string value { get; init; }
    public byte[] valueByte { get; init; }

    public MessageValueObject(string value)
    {
        if (value == null || value.Length == 0)
            throw new ArgumentException("Message cannot be empty.", nameof(value));

        this.value = value;
        this.valueByte = Encoding.UTF8.GetBytes(value);
    }

    public MessageValueObject(byte[] valueByte)
    {
        if (value == null || value.Length == 0)
            throw new ArgumentException("Message cannot be empty.", nameof(value));

        var asString = Encoding.UTF8.GetString(valueByte);

        this.value = asString;
        this.valueByte = valueByte;
    }
}
