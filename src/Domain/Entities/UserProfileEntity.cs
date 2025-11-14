using System;
using Domain.Common;

namespace Domain.Entities;

public class UserProfileEntity : AggregateRoot
{
    public Guid UserId { get; private set; }
    public Guid PersonalId { get; private set; }
    public bool IsActive { get; private set; }

    public UserProfileEntity(Guid userId, Guid personalId)
    {
        id = Guid.NewGuid();
        UserId = userId;
        PersonalId = personalId;
        IsActive = true;
    }

    public void Deactivate() => IsActive = false;
}
