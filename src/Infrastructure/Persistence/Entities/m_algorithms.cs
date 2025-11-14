using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class m_algorithms : AuditableEntity
{
    public DateTime effectiveAt { get; set; }
    public DateTime? expiresAt { get; set; }
    public required string name { get; set; }
    public required byte[] algorithm { get; set; }
    public string keyRequired { get; set; } = default!;
}
