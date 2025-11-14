using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_personal_addresses : AuditableEntity
{
    public Guid personalId { get; set; }
    public Guid addressId { get; set; }
}
