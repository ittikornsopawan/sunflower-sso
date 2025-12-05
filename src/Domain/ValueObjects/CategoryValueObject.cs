namespace Domain.ValueObjects;

/// <summary>
/// Value Object representing a category name.
/// Category value can be null.
/// </summary>
public sealed record CategoryValueObject
{
    /// <summary>
    /// Category name (nullable).
    /// </summary>
    public string? value { get; init; }

    /// <summary>
    /// Creates a new CategoryValueObject. Value can be null.
    /// </summary>
    /// <param name="value">Category name or null.</param>
    public CategoryValueObject(string? value)
    {
        // No validation — null is allowed
        this.value = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    /// <summary>
    /// Checks whether category was provided or is null.
    /// </summary>
    public bool IsEmpty => this.value == null;
}