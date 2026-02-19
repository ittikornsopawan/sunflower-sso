using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

/// <summary>
/// Value Object representing a 6-digit numeric Otp code.
/// </summary>
/// <author>Ittikorn Sopawan</author>
public sealed record OtpCodeValueObject
{
    /// <summary>
    /// The Otp code value (6-digit numeric string).
    /// </summary>
    public string value { get; init; }

    /// <summary>
    /// Creates a new instance of <see cref="OtpCodeValueObject"/> and validates that it is a 6-digit number.
    /// </summary>
    /// <param name="value">Otp code value</param>
    /// <exception cref="ArgumentException">Thrown if the value is not a 6-digit numeric string.</exception>
    /// <returns>An instance of <see cref="OtpCodeValueObject"/>.</returns>
    public OtpCodeValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Otp code cannot be empty.", nameof(value));

        if (!Regex.IsMatch(value, @"^\d{6}$"))
            throw new ArgumentException("Otp code must be exactly 6 digits.", nameof(value));

        this.value = value;
    }

    /// <summary>
    /// Constructor that generates a random 6-digit Otp code.
    /// </summary>
    public OtpCodeValueObject()
    {
        this.value = GenerateRandom();
    }

    /// <summary>
    /// Generates a random 6-digit Otp code.
    /// </summary>
    /// <returns>A new instance of <see cref="OtpCodeValueObject"/> with a random 6-digit code.</returns>
    private string GenerateRandom()
    {
        var random = new Random();
        return random.Next(0, 1000000).ToString("D6");
    }
}
