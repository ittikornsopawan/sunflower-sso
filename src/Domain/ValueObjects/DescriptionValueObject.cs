namespace Domain.ValueObjects;

/// <summary>
/// Value Object representing an optional description.
/// </summary>
/// <author>Ittikorn Sopawan</author>
public sealed record DescriptionValueObject
{
    /// <summary>
    /// Description text. Can be null.
    /// </summary>
    public string? value { get; init; }

    /// <summary>
    /// Creates a new DescriptionValueObject.
    /// Allows null and no length limitation.
    /// </summary>
    /// <param name="value">The description text (nullable).</param>
    public DescriptionValueObject(string? value)
    {
        this.value = value;
    }

    /// <summary>
    /// Checks whether description was provided or is null.
    /// </summary>
    public bool IsEmpty => this.value == null;
}
