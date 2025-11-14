using System;

namespace Domain.ValueObjects;

public record TokenClaimValueObject
{
    public IReadOnlyDictionary<string, string> claims { get; init; }

    public TokenClaimValueObject(Dictionary<string, string> claims)
    {
        if (claims == null || claims.Count == 0)
            throw new ArgumentException("Claims cannot be empty.", nameof(claims));

        this.claims = new Dictionary<string, string>(claims);
    }

    public override string ToString() => string.Join(", ", this.claims);

}
