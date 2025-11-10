using System;
using Domain.Common;

namespace Domain.Entities;

public class t_user_referrer_mappings : AuditableEntity
{
    public Guid userId { get; set; }
    public Guid referrerId { get; set; }
}
