using System;
using Domain.Common;

namespace Domain.Entities;

public class t_personal_addresses : AuditableEntity
{
    public Guid personalId { get; set; }
    public Guid addressId { get; set; }
}
