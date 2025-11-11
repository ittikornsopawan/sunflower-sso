using System;
using Domain.Common;

namespace Domain.Entities;

public class m_keys : AuditableEntity
{
    public DateTime effectiveAt { get; set; }
    public DateTime? expiresAt { get; set; }
    public Guid typeId { get; set; }
    public required byte[] key { get; set; }
}
