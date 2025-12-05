using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_user_consents : AuditableEntity
{
    public required Guid userId { get; set; }
    public required Guid consentTypeId { get; set; }
    public required Guid consentId { get; set; }
    public required string version { get; set; }
    public bool result { get; set; }
}
