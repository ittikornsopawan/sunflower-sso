namespace Domain.ValueObjects;

/// <summary>
/// Value Object representing an effective period with start and optional end.
/// </summary>
public sealed record EffectivePeriodValueObject
{
    /// <summary>
    /// The datetime when the object becomes effective (NOT NULL).
    /// </summary>
    public DateTime effectiveAt { get; init; }

    /// <summary>
    /// Optional expiration time. Must be greater than effectiveAt if provided.
    /// </summary>
    public DateTime? expiresAt { get; init; }

    /// <summary>
    /// Creates a new effective period value object.
    /// </summary>
    /// <param name="effectiveAt">Start datetime (required, cannot be null).</param>
    /// <param name="expiresAt">Optional expiration datetime (must be > effectiveAt).</param>
    /// <exception cref="ArgumentException">Invalid date rules.</exception>
    public EffectivePeriodValueObject(DateTime? effectiveAt = null, DateTime? expiresAt = null)
    {
        var effective = effectiveAt ?? DateTime.UtcNow;

        if (expiresAt != null && expiresAt <= effective)
            throw new ArgumentException("expiresAt must be null or later than effectiveAt.");

        this.effectiveAt = effective;
        this.expiresAt = expiresAt;
    }

    /// <summary>
    /// Returns true if expired relative to now.
    /// </summary>
    public bool IsExpired => expiresAt != null && DateTime.UtcNow > expiresAt;

    /// <summary>
    /// Returns true if the current period is active.
    /// expiresAt == null means never expires.
    /// </summary>
    public bool IsActive => !IsExpired;
}
