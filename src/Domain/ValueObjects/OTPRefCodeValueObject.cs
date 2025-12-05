using System.Text;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

/// <summary>
/// Value Object representing an 8-character alphanumeric OTP reference code.
/// </summary>
/// <author>Ittikorn Sopawan</author>
public sealed record OTPRefCodeValueObject
{
    /// <summary>
    /// The reference code value (8-character alphanumeric string).
    /// </summary>
    public string value { get; init; }

    private const int Length = 8;
    private const string AllowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    /// <summary>
    /// Constructor that generates a random 8-character alphanumeric reference code.
    /// </summary>
    public OTPRefCodeValueObject()
    {
        this.value = GenerateRandom();
    }

    /// <summary>
    /// Constructor that sets a specific reference code value.
    /// </summary>
    /// <param name="value">Reference code value</param>
    /// <exception cref="ArgumentException">Thrown if the value is not exactly 8 alphanumeric characters.</exception>
    public OTPRefCodeValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Reference code cannot be empty.", nameof(value));

        if (!Regex.IsMatch(value, @"^[A-Za-z0-9]{8}$"))
            throw new ArgumentException("Reference code must be exactly 8 alphanumeric characters (A-Z, a-z, 0-9).", nameof(value));

        this.value = value;
    }

    /// <summary>
    /// Generates a random 8-character alphanumeric reference code.
    /// </summary>
    /// <returns>8-character alphanumeric string</returns>
    private string GenerateRandom()
    {
        var random = new Random();
        var sb = new StringBuilder(Length);
        for (int i = 0; i < Length; i++)
        {
            sb.Append(AllowedChars[random.Next(AllowedChars.Length)]);
        }
        return sb.ToString();
    }
}
