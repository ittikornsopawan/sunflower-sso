using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_policies : AuditableEntity
{
    public required string name { get; set; }
    public string description { get; set; } = default!;
    public required string effect { get; set; }
    public required string action { get; set; }
    public required string resource { get; set; }
    public string conditionLogic { get; set; } = default!;
}
