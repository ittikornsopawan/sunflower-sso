namespace Domain.ValueObjects;

public sealed record class ParameterKeyValueObject
{
    public string value { get; init; }

    public ParameterKeyValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Key cannot be empty", nameof(value));

        if (value.Length > 1024)
            throw new ArgumentException("Key max length is 1024", nameof(value));

        this.value = value;
    }

    public override string ToString() => this.value;
}
