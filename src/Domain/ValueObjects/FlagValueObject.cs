namespace Domain.ValueObjects;

/// <summary>
/// Immutable Boolean Flag ValueObject.
/// Can be used for flags like IsEmpty, IsSpecial, IsActive, etc.
/// </summary>
/// <remarks>
/// Author: Ittikorn Sopawan
/// </remarks>
public sealed record FlagValueObject
{
    /// <summary>
    /// The boolean value of the flag.
    /// </summary>
    public bool value { get; init; }

    /// <summary>
    /// The type of the flag (e.g., "IsEmpty", "IsSpecial").
    /// </summary>
    public string flagType { get; init; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="value">Flag value</param>
    /// <param name="flagType">Flag type</param>
    /// <exception cref="ArgumentException">Thrown when flagType is null or empty</exception>
    public FlagValueObject(bool value, string flagType)
    {
        if (string.IsNullOrWhiteSpace(flagType)) throw new ArgumentException("flagType cannot be null or empty.", nameof(flagType));

        this.value = value;
        this.flagType = flagType;
    }

    /// <summary>
    /// Returns a new FlagValueObject with updated value
    /// </summary>
    /// <param name="newValue">New boolean value</param>
    /// <returns>New FlagValueObject instance</returns>
    public FlagValueObject Set(bool newValue) => this with { value = newValue };

    public override string ToString() => $"flagType: {flagType}, value: {value}";
}