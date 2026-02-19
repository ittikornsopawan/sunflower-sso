namespace Domain.ValueObjects;

/// <summary>
/// Value Object representing the Purpose of an action (e.g., Otp purpose).
/// The value must be one of the allowed purposes.
/// </summary>
/// <author>ChatGPT</author>
public sealed record OtpPurposeValueObject
{
    /// <summary>
    /// The purpose value.
    /// </summary>
    public string value { get; init; }

    /// <summary>
    /// List of allowed purposes.
    /// </summary>
    private static readonly HashSet<string> AllowedPurposes = new()
    {
        "LOGIN",
        "VERIFY",
        "CONFIRM",
        "RESET_PASSWORD",
        "OTHER"
    };

    /// <summary>
    /// Creates a new instance of <see cref="PurposeValueObject"/> and validates the value.
    /// </summary>
    /// <param name="value">The purpose value to create.</param>
    /// <exception cref="ArgumentException">Thrown when the value is null, empty, or not in the allowed purposes list.</exception>
    /// <returns>An instance of <see cref="PurposeValueObject"/>.</returns>
    /// <author>ChatGPT</author>
    public OtpPurposeValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Purpose cannot be empty.", nameof(value));

        if (!AllowedPurposes.Contains(value))
            throw new ArgumentException($"Purpose '{value}' is not allowed. Allowed purposes: {string.Join(", ", AllowedPurposes)}", nameof(value));

        this.value = value;
    }
}
