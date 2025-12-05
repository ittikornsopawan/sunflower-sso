namespace Domain.ValueObjects;

public record class ValueValueObject
{
    public string value { get; }

    public ValueValueObject(string value)
    {
        if (value == null) throw new ArgumentException("Value cannot be null.", nameof(value));

        this.value = value;
    }
}
