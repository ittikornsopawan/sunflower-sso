using System;

namespace Domain.ValueObjects;

public record AccessTokenValueObject
{
    public string Token { get; init; }
    public DateTime Expiry { get; init; }

    public AccessTokenValueObject(string token, DateTime expiry)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Access token cannot be empty.", nameof(token));

        if (expiry <= DateTime.UtcNow)
            throw new ArgumentException("Expiry must be in the future.", nameof(expiry));

        Token = token;
        Expiry = expiry;
    }

    public bool IsExpired => DateTime.UtcNow >= Expiry;

    public override string ToString() => Token;
}
