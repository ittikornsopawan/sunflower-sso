using System;
using Domain.Common;

namespace Domain.Entities;

public class t_users : AuditableEntity
{
    public required string username { get; set; }
    public required string authenticationType { get; set; }
}
