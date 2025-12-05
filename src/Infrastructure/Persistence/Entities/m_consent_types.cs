using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class m_consent_types : AuditableEntity
{
    public required string name { get; set; }
    public string? description { get; set; }
    public bool isRequired { get; set; }
    public required string latestVersion { get; set; }
}
