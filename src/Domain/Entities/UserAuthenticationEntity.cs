using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class UserAuthenticationEntity : AggregateRoot
{
    public EffectivePeriodValueObject period { get; init; }
    public UserIdValueObject userId { get; init; }
    public FlagsAttribute isTemporary { get; init; }
    public AlgorithmValueObject algorithm { get; init; }
    public PasswordValueObject password { get; private set; }

    public UserAuthenticationEntity(
        UserIdValueObject userId,
        PasswordValueObject password,
        EffectivePeriodValueObject period,
        AlgorithmValueObject algorithm,
        FlagsAttribute isTemporary
    )
    {
        this.period = period;
        this.userId = userId;
        this.isTemporary = isTemporary;
        this.algorithm = algorithm;
        this.password = password;
    }

    public void UpdatePassword(PasswordValueObject newPassword)
    {
        this.password = newPassword;
    }
}