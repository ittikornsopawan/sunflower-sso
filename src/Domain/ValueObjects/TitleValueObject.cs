namespace Domain.ValueObjects;

/// <summary>
/// Value Object representing a title. Allows null.
/// </summary>
public sealed record TitleValueObject
{
    /// <summary>
    /// The title value. Can be null.
    /// </summary>
    public string? value { get; init; }

    /// <summary>
    /// Creates a new TitleValueObject.
    /// - Allows null
    /// - Trims whitespace
    /// </summary>
    /// <param name="value">The title value (nullable).</param>
    public TitleValueObject(string? value)
    {
        this.value = value;
    }

    /// <summary>
    /// Checks whether category was provided or is null.
    /// </summary>
    public bool IsEmpty => this.value == null;
}
