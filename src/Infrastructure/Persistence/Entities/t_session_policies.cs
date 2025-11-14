using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_session_policies : AuditableEntity
{
    public Guid sessionId { get; set; }
    public Guid policyId { get; set; }
    public required string values { get; set; }
}
