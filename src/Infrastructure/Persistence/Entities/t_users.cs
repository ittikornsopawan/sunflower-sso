using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_users : AuditableEntity
{
    public required byte[] username { get; set; }
    public required string authenticationType { get; set; }
}
