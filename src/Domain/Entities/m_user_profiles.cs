using System;
using Domain.Common;

namespace Domain.Entities;

public class m_user_profiles : AuditableEntity
{
    public Guid userId { get; set; }
    public Guid personalId { get; set; }
}
