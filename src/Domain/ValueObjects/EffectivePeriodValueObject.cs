namespace Domain.ValueObjects;

public record class EffectivePeriodValueObject
{
    public DateTime effectiveAt { get; init; }
    public DateTime? expiresAt { get; init; }

    public EffectivePeriodValueObject(DateTime effectiveAt, DateTime? expiresAt = null)
    {
        if (expiresAt != null && expiresAt <= effectiveAt)
            throw new ArgumentException("ExpiresAt must be after EffectiveAt");

        this.effectiveAt = effectiveAt;
        this.expiresAt = expiresAt;
    }
}
