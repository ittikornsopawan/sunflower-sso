using System;

namespace Domain.ValueObjects;

public sealed record AccessTokenValueObject
{
    public string token { get; init; }
    public DateTime expiry { get; init; }

    public AccessTokenValueObject(string token, DateTime expiry)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Access token cannot be empty.", nameof(token));

        if (expiry <= DateTime.UtcNow)
            throw new ArgumentException("Expiry must be in the future.", nameof(expiry));

        this.token = token;
        this.expiry = expiry;
    }

    public bool IsExpired => DateTime.UtcNow >= this.expiry;

    public override string ToString() => this.token;
}
