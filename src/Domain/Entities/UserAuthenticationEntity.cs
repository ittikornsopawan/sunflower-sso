using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class UserAuthenticationEntity : AggregateRoot
{
    public UsernameValueObject username { get; private set; }
    public PasswordHashValueObject passwordHash { get; private set; }

    public UserAuthenticationEntity(UsernameValueObject username, PasswordHashValueObject passwordHash)
    {
        this.username = username;
        this.passwordHash = passwordHash;
    }

    public void UpdatePassword(PasswordHashValueObject newPassword)
    {
        this.passwordHash = newPassword;
    }
}