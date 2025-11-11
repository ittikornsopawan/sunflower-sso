using System;
using Domain.Common;

namespace Domain.Entities;

public class m_parameters : AuditableEntity
{
    public DateTime effectiveAt { get; set; }
    public DateTime? expiresAt { get; set; }
    public string? category { get; set; }
    public required string key { get; set; }
    public string? language { get; set; }
    public string? value { get; set; }
}
