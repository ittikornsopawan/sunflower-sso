using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_consents : AuditableEntity
{
    public Guid consentTypeId { get; set; }
    public required string version { get; set; }
    public required string name { get; set; }
    public string? description { get; set; }
    public required string content { get; set; }
    public required string language { get; set; }
}
