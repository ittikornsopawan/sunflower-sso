namespace Domain.ValueObjects;

/// <summary>
/// Generic ValueObject that represents a name.
/// Can be used for algorithm name or any other named field.
/// </summary>
/// <remarks>
/// Author: Ittikorn Sopawan
/// </remarks>
public sealed record NameValueObject
{
    /// <summary>
    /// The underlying name string.
    /// </summary>
    public string value { get; }

    /// <summary>
    /// Constructor that validates the name.
    /// </summary>
    /// <param name="value">The name string.</param>
    /// <exception cref="ArgumentException">Thrown when the name is null, empty, or too long.</exception>
    public NameValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Name cannot be null or empty.", nameof(value));

        if (value.Length > 128)
            throw new ArgumentException("Name cannot exceed 128 characters.", nameof(value));

        this.value = value.Trim();
    }

    /// <summary>
    /// Returns a new NameValueObject with updated value.
    /// </summary>
    /// <param name="value">The new label value.</param>
    /// <returns>A new NameValueObject with the updated value.</returns>
    public NameValueObject UpdateValue(string value) => new NameValueObject(value);


    /// <summary>
    /// Returns the name as string.
    /// </summary>
    public override string ToString() => this.value;
}
