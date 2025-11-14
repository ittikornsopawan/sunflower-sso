using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class m_key_types : AuditableEntity
{
    public DateTime effectiveAt { get; set; }
    public DateTime? expiresAt { get; set; }
    public required string name { get; set; }
    public string title { get; set; } = default!;
    public string description { get; set; } = default!;
}
