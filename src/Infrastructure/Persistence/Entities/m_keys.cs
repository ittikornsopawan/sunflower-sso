using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class m_keys : AuditableEntity
{
    public DateTime effectiveAt { get; set; }
    public DateTime? expiresAt { get; set; }
    public Guid typeId { get; set; }
    public required byte[] key { get; set; }
}
