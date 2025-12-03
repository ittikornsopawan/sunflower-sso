using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class UserEntity : AggregateRoot
{
    public UserIdValueObject userId { get; init; }
    public UserCodeValueObject code { get; private set; }
    public UsernameValueObject username { get; private set; }
    public AuthenticationTypeValueObject authenticationType { get; private set; }

    public UserEntity(
        UserCodeValueObject code,
        UsernameValueObject username,
        AuthenticationTypeValueObject authenticationType,
        UserIdValueObject? userId = null
    )
    {
        this.userId = userId ?? new UserIdValueObject(this.id);
        this.id = userId?.value ?? Guid.NewGuid();
        this.code = code;
        this.username = username;
        this.authenticationType = authenticationType;
    }

    public void UpdateUsername(UsernameValueObject newUsername)
    {
        this.username = newUsername ?? throw new ArgumentNullException(nameof(newUsername));
    }
}
