using System;
using Domain.Common;

namespace Domain.Entities;

public class m_key_types : AuditableEntity
{
    public DateTimeOffset effectiveAt { get; set; }
    public DateTimeOffset? expiresAt { get; set; }
    public required string name { get; set; }
    public string title { get; set; } = default!;
    public string description { get; set; } = default!;
}
