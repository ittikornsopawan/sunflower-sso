using System;
using Domain.Common;

namespace Domain.Entities;

public class t_personal_contacts : AuditableEntity
{
    public Guid personalId { get; set; }
    public Guid contactId { get; set; }
}
