using System;
using Domain.Common;

namespace Domain.Entities;

public class t_session_attributes : AuditableEntity
{
    public Guid sessionId { get; set; }
    public Guid attributeId { get; set; }
    public required string values { get; set; }
}
