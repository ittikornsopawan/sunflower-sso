using System;
using Domain.Common;

namespace Domain.Entities;

public class m_parameters : AuditableEntity
{
    public DateTimeOffset effectiveAt { get; set; }
    public DateTimeOffset? expiresAt { get; set; }
    public string category { get; set; } = default!;
    public required string key { get; set; }
    public string language { get; set; } = default!;
    public string value { get; set; } = default!;
}
