using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_user_attribute_mappings : AuditableEntity
{
    public Guid userId { get; set; }
    public Guid attributeId { get; set; }
    public required string value { get; set; }
}
