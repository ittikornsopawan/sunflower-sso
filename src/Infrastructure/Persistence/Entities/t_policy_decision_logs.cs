using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_policy_decision_logs : AuditableEntity
{
    public Guid userId { get; set; }
    public Guid policyId { get; set; }
    public string resource { get; set; } = default!;
    public string action { get; set; } = default!;
    public required string decision { get; set; }
    public string evaluatedAttributes { get; set; } = default!;
    public string reason { get; set; } = default!;
}
