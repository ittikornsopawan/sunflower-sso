using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_change_logs : AuditableEntity
{
    public Guid requestId { get; set; }
    public Guid recordId { get; set; }
    public required string tableName { get; set; }
    public required string operation { get; set; }
}
