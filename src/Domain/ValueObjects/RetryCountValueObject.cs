using System;

namespace Domain.ValueObjects;

public sealed record RetryCountValueObject
{
    public int value { get; init; }

    public RetryCountValueObject(int? value = 0)
    {
        this.value = value ?? 0;
    }

    public RetryCountValueObject Increase()
    {
        return new RetryCountValueObject(this.value + 1);
    }
}
