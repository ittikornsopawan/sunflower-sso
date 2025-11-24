using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class UserAuthenticationEntity : AggregateRoot
{
    public PasswordValueObject password { get; private set; }
    public EffectivePeriodValueObject period { get; private set; }
    public UserIdValueObject userId { get; private set; }
    public AlgorithmValueObject algorithm { get; private set; }

    public UserAuthenticationEntity(UserIdValueObject userId, PasswordValueObject password, EffectivePeriodValueObject period, AlgorithmValueObject algorithm)
    {
        this.userId = userId;
        this.password = password;
        this.period = period;
        this.algorithm = algorithm;
    }

    public void UpdatePassword(PasswordValueObject newPassword)
    {
        this.password = newPassword;
    }
}