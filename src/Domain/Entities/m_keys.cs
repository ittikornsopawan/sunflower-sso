using System;

namespace Domain.Entities;

public class m_keys
{
    public DateTimeOffset effectiveAt { get; set; }
    public DateTimeOffset? expiresAt { get; set; }
    public Guid type_id { get; set; }
    public required byte[] key { get; set; }
}
