using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class m_parameters : AuditableEntity
{
    public DateTime effectiveAt { get; set; }
    public DateTime? expiresAt { get; set; }
    public string? category { get; set; }
    public required string key { get; set; }
    public string? title { get; set; }
    public string? description { get; set; }
    public string? language { get; set; }
    public string? value { get; set; }
}
