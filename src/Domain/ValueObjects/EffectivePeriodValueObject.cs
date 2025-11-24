namespace Domain.ValueObjects;

/// <summary>
/// Value object representing an effective period with a start and optional end date.
/// </summary>
/// <author>Ittikorn Sopawan</author>
public sealed record EffectivePeriodValueObject
{
    /// <summary>
    /// The start date of the period.
    /// </summary>
    public DateTime effectiveAt { get; init; }

    /// <summary>
    /// The optional end date of the period. Must be after EffectiveAt if specified.
    /// </summary>
    public DateTime? expiresAt { get; init; }

    /// <summary>
    /// Initializes a new instance of the EffectivePeriodValueObject.
    /// </summary>
    /// <param name="effectiveAt">The start date of the period.</param>
    /// <param name="expiresAt">The optional end date of the period. Must be after effectiveAt if specified.</param>
    /// <exception cref="ArgumentException">Thrown if expiresAt is before or equal to effectiveAt.</exception>
    /// <author>Ittikorn Sopawan</author>
    public EffectivePeriodValueObject(DateTime effectiveAt, DateTime? expiresAt = null)
    {
        if (expiresAt != null && expiresAt <= effectiveAt)
            throw new ArgumentException("expiresAt must be after effectiveAt", nameof(expiresAt));

        this.effectiveAt = effectiveAt;
        this.expiresAt = expiresAt;
    }

    /// <summary>
    /// Checks if the current UTC date/time is within the effective period.
    /// </summary>
    /// <returns>True if the current UTC date/time is within the period; otherwise false.</returns>
    /// <author>Ittikorn Sopawan</author>
    public bool IsEffective()
    {
        var now = DateTime.UtcNow;
        return now >= effectiveAt && (expiresAt == null || now <= expiresAt.Value);
    }

    /// <summary>
    /// Returns a string representation of the effective period.
    /// </summary>
    /// <returns>String describing the effective period.</returns>
    /// <author>Ittikorn Sopawan</author>
    public override string ToString()
    {
        return expiresAt != null
            ? $"{effectiveAt:yyyy-MM-dd HH:mm:ss} - {expiresAt:yyyy-MM-dd HH:mm:ss}"
            : $"{effectiveAt:yyyy-MM-dd HH:mm:ss} - (no expiration)";
    }
}
