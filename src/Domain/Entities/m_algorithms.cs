using System;
using Domain.Common;

namespace Domain.Entities;

public class m_algorithms : AuditableEntity
{
    public DateTime effectiveAt { get; set; }
    public DateTime? expiresAt { get; set; }
    public required string name { get; set; }
    public required byte[] algorithm { get; set; }
    public string keyRequired { get; set; } = default!;
}
