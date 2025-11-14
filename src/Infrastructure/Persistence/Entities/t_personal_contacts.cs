using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_personal_contacts : AuditableEntity
{
    public Guid personalId { get; set; }
    public Guid contactId { get; set; }
}
