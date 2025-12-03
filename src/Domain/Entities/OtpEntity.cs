using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed record OtpEntity : AggregateRoot
{
    public Guid UserId { get; private set; }
    public OtpCodeValueObject Code { get; private set; }
    public DateTime ExpiredAt { get; private set; }
    public string RefCode { get; private set; }

    public OtpEntity(Guid userId, OtpCodeValueObject code, DateTime expiredAt, string refCode)
    {
        id = Guid.NewGuid();
        UserId = userId;
        Code = code;
        ExpiredAt = expiredAt;
        RefCode = refCode;
    }

    public bool IsExpired() => DateTime.UtcNow > ExpiredAt;
}