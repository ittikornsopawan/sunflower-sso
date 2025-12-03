using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class UserReferrerEntity : AggregateRoot
{
    public UserIdValueObject userId { get; init; }
    public UserIdValueObject referrerId { get; init; }

    public UserReferrerEntity(UserIdValueObject userId, UserIdValueObject referrerId)
    {
        this.userId = userId;
        this.referrerId = referrerId;
    }
}
