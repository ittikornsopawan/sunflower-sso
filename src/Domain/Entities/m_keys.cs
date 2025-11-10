using System;
using Domain.Common;

namespace Domain.Entities;

public class m_keys : AuditableEntity
{
    public DateTimeOffset effectiveAt { get; set; }
    public DateTimeOffset? expiresAt { get; set; }
    public Guid typeId { get; set; }
    public required byte[] key { get; set; }
}
