using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_user_referrer_mappings : AuditableEntity
{
    public Guid userId { get; set; }
    public Guid referrerId { get; set; }
}
