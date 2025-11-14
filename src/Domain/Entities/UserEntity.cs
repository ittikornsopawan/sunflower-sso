using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class UserEntity : AggregateRoot
{
    public UserCodeValueObject code { get; private set; }
    public UsernameValueObject username { get; private set; }

    public UserEntity(UserCodeValueObject code, UsernameValueObject username, Guid? id = null)
    {
        this.id = id ?? Guid.NewGuid();
        this.code = code;
        this.username = username;
    }
}
