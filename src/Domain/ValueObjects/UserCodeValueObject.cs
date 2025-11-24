namespace Domain.ValueObjects;

public sealed record class UserCodeValueObject
{
    public string value { get; init; }

    public UserCodeValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Code cannot be empty.", nameof(value));

        if (value.Length < 3)
            throw new ArgumentException("Code must be at least 3 characters.", nameof(value));

        this.value = value;
    }

    public override string ToString() => this.value;
}
