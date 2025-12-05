namespace Domain.ValueObjects;

public sealed record StatusValueObject
{
    public string value { get; init; }

    private static readonly HashSet<string> AllowedStatuses = new()
    {
        "PENDING", "SENT", "FAILED"
    };

    public StatusValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Status cannot be empty.", nameof(value));

        value = value.Trim().ToUpper();

        if (!AllowedStatuses.Contains(value))
            throw new ArgumentException($"Invalid status '{value}'. Allowed: PENDING, SENT, FAILED.", nameof(value));

        this.value = value;
    }
}
