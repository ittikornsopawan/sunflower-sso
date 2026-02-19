namespace Domain.ValueObjects;

/// <summary>
/// Value Object representing Otp attempts.
/// Encapsulates the current attempt count and maximum allowed attempts.
/// Immutable and self-validated.
/// </summary>
public sealed record OtpAttemptValueObject
{
    /// <summary>
    /// Current number of attempts.
    /// </summary>
    public int value { get; init; }

    /// <summary>
    /// Maximum allowed attempts.
    /// </summary>
    private int maxAttempts { get; init; }

    /// <summary>
    /// Creates a new OtpAttemptValueObject with validation.
    /// </summary>
    /// <param name="value">Current attempt count.</param>
    /// <param name="maxAttempts">Maximum allowed attempts. Default = 5.</param>
    /// <exception cref="ArgumentException">If value is invalid or exceeds max attempts.</exception>
    public OtpAttemptValueObject(int value, int? maxAttempts = null)
    {
        int max = maxAttempts ?? 5;

        if (value < 0)
            throw new ArgumentException("Otp attempt cannot be negative.", nameof(value));

        if (max <= 0)
            throw new ArgumentException("Max attempts must be greater than zero.", nameof(maxAttempts));

        if (value > max)
            throw new ArgumentException("Otp attempts cannot exceed max attempts.", nameof(value));

        this.value = value;
        this.maxAttempts = max;
    }

    /// <summary>
    /// Increments the attempt count and returns a new instance (immutable).
    /// </summary>
    /// <returns>New OtpAttemptValueObject with incremented attempts.</returns>
    /// <exception cref="InvalidOperationException">If maximum attempts exceeded.</exception>
    public OtpAttemptValueObject Increment()
    {
        if (this.value >= this.maxAttempts)
            throw new InvalidOperationException("Maximum Otp attempts exceeded.");

        return new OtpAttemptValueObject(this.value + 1, this.maxAttempts);
    }

    /// <summary>
    /// Checks if the maximum attempts are reached.
    /// </summary>
    public bool IsExceeded => this.value >= this.maxAttempts;
}