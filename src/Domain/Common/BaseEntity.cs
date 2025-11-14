using System;

namespace Domain.Common;

public abstract class BaseEntity
{
    public Guid id { get; protected set; }

    protected BaseEntity()
    {
        id = Guid.NewGuid();
    }
}
