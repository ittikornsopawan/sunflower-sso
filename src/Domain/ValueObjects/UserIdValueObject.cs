using System;

namespace Domain.ValueObjects;

/// <summary>
/// ValueObject that represents a User Identifier (UUID / GUID).
/// Ensures that only valid, non-empty GUID values are used inside the Domain.
/// </summary>
/// <remarks>
/// Author: Ittikorn Sopawan
/// </remarks>
public sealed record UserIdValueObject
{
    /// <summary>
    /// The underlying GUID value.
    /// </summary>
    public Guid value { get; }

    /// <summary>
    /// Creates a UserIdValueObject from a Guid.
    /// </summary>
    /// <param name="value">A non-empty GUID.</param>
    /// <exception cref="ArgumentException">Thrown when the GUID is empty.</exception>
    public UserIdValueObject(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("UserId cannot be an empty GUID.", nameof(value));

        this.value = value;
    }

    /// <summary>
    /// Parameterless constructor that automatically generates
    /// a new non-empty GUID for this UserId.
    /// </summary>
    public UserIdValueObject()
    {
        this.value = Guid.NewGuid();
    }

    /// <summary>
    /// Creates a UserIdValueObject from a UUID string.
    /// </summary>
    /// <param name="value">A string representing a GUID (e.g., "d2a5dd97-62a7-4d8d-bc36-...").</param>
    /// <exception cref="ArgumentException">Thrown when the string is not a valid GUID.</exception>
    public UserIdValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("UserId cannot be null or empty.", nameof(value));

        if (!Guid.TryParse(value, out var guid))
            throw new ArgumentException("UserId must be a valid GUID string.", nameof(value));

        if (guid == Guid.Empty)
            throw new ArgumentException("UserId cannot be an empty GUID.", nameof(value));

        this.value = guid;
    }

    /// <summary>
    /// Returns the GUID value.
    /// </summary>
    /// <returns>The underlying Guid.</returns>
    public Guid ToGuid() => this.value;

    /// <summary>
    /// Returns the GUID as a string.
    /// </summary>
    /// <returns>UUID string in canonical format.</returns>
    public override string ToString() => this.value.ToString();
}
