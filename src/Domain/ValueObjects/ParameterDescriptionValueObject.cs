namespace Domain.ValueObjects;

public sealed record ParameterDescriptionValueObject
{
    public string value { get; init; }

    public ParameterDescriptionValueObject(string value)
    {
        if (!string.IsNullOrWhiteSpace(value) && value.Length > 512)
            throw new ArgumentException("Description max length is 512", nameof(value));

        this.value = value;
    }

    public override string ToString() => this.value;
}