using System;
using Domain.Common;

namespace Domain.Entities;

public class t_push_notifications : AuditableEntity
{
    public required string type { get; set; }
    public required string message { get; set; }
    public Guid? userId { get; set; }
    public Guid? contactId { get; set; }
    public string deliveryStatus { get; set; } = default!;
    public string metadata { get; set; } = default!;
}
