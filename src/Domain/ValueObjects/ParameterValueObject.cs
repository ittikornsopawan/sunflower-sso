using System;

namespace Domain.ValueObjects;

public sealed record ParameterValue
{
    public string value { get; init; }

    public ParameterValue(string value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        this.value = value;
    }

    public override string ToString() => this.value;
}
