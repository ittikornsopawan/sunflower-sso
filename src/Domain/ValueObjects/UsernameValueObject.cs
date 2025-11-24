using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

/// <summary>
/// Value object representing a username.
/// Enforces rules:
/// - Allowed characters: lowercase a-z, digits 0-9, underscores (_), dots (.)
/// - Must contain at least 6 letters a-z
/// </summary>
/// <author>Ittikorn Sopawan</author>
public sealed record UsernameValueObject
{
    /// <summary>
    /// The username as string.
    /// </summary>
    public string value { get; private set; }

    /// <summary>
    /// The username as byte array (UTF8).
    /// </summary>
    public byte[] valueBytes { get; private set; }

    /// <summary>
    /// Create a UsernameValueObject from string.
    /// </summary>
    /// <param name="value">Input username string.</param>
    /// <exception cref="ArgumentException">Thrown if validation fails.</exception>
    /// <author>Ittikorn Sopawan</author>
    public UsernameValueObject(string value)
    {
        ValidateString(value);
        this.value = value;
        this.valueBytes = Encoding.UTF8.GetBytes(value);
    }

    /// <summary>
    /// Create a UsernameValueObject from byte array.
    /// </summary>
    /// <param name="value">Input username as byte array (UTF8).</param>
    /// <exception cref="ArgumentException">Thrown if validation fails.</exception>
    /// <author>Ittikorn Sopawan</author>
    public UsernameValueObject(byte[] value)
    {
        if (value == null || value.Length == 0)
            throw new ArgumentException("Username bytes cannot be empty.", nameof(value));

        var str = Encoding.UTF8.GetString(value);
        ValidateString(str);

        this.value = str;
        this.valueBytes = (byte[])value.Clone();
    }

    /// <summary>
    /// Returns a clone of the username bytes.
    /// </summary>
    /// <returns>Byte array representing the username.</returns>
    /// <author>Ittikorn Sopawan</author>
    public byte[] ToBytes() => (byte[])valueBytes.Clone();

    /// <summary>
    /// Returns the username as string.
    /// </summary>
    /// <returns>Username string.</returns>
    /// <author>Ittikorn Sopawan</author>
    public override string ToString() => value;

    #region Validation

    /// <summary>
    /// Validates username rules:
    /// - Only lowercase letters a-z, digits 0-9, underscores (_) or dots (.)
    /// - At least 6 letters a-z
    /// </summary>
    /// <param name="value">Username string to validate.</param>
    /// <exception cref="ArgumentException">Thrown if validation fails.</exception>
    /// <author>Ittikorn Sopawan</author>
    private static void ValidateString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Username cannot be empty.", nameof(value));

        if (!Regex.IsMatch(value, @"^[a-z0-9_.]+$"))
            throw new ArgumentException("Username can only contain lowercase letters (a-z), digits (0-9), underscores (_) or dots (.)", nameof(value));

        int alphaCount = 0;
        foreach (var c in value)
        {
            if (c >= 'a' && c <= 'z') alphaCount++;
        }

        if (alphaCount < 6)
            throw new ArgumentException("Username must contain at least 6 lowercase letters (a-z).", nameof(value));
    }

    #endregion
}