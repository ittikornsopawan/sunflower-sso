using System;
using Domain.Common;

namespace Domain.Entities;

public class t_user_attribute_mappings : AuditableEntity
{
    public Guid userId { get; set; }
    public Guid attributeId { get; set; }
    public required string value { get; set; }
}
