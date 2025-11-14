namespace Domain.ValueObjects;

public record class ParameterCategoryValueObject
{
    public string value { get; init; }

    public ParameterCategoryValueObject(string value)
    {
        if (!string.IsNullOrWhiteSpace(value) && value.Length > 32)
            throw new ArgumentException("Category max length is 32", nameof(value));

        this.value = value;
    }

    public override string ToString() => this.value;
}
