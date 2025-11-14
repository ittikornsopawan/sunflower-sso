using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class m_user_profiles : AuditableEntity
{
    public Guid userId { get; set; }
    public Guid personalId { get; set; }
}
