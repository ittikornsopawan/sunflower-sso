using System;
using Domain.Common;

namespace Domain.Entities;

public class t_policy_attribute_mappings : AuditableEntity
{
    public Guid policyId { get; set; }
    public Guid attributeId { get; set; }
    public required string operatorValue { get; set; }
    public required string expectedValue { get; set; }
    public string logicGroup { get; set; } = default!;
}