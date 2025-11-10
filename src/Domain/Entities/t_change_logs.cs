using System;
using Domain.Common;

namespace Domain.Entities;

public class t_change_logs : AuditableEntity
{
    public Guid requestId { get; set; }
    public Guid recordId { get; set; }
    public required string tableName { get; set; }
    public required string operation { get; set; }
}
