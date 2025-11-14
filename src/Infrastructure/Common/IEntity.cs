using System;

namespace Infrastructure.Common;

public interface IEntity
{
    Guid id { get; set; }
}
