namespace Domain.ValueObjects;

public record class ParameterTitleValueObject
{
    public string value { get; init; }

    public ParameterTitleValueObject(string value)
    {
        if (!string.IsNullOrWhiteSpace(value) && value.Length > 128)
            throw new ArgumentException("Title max length is 128", nameof(value));

        this.value = value;
    }

    public override string ToString() => this.value;
}
