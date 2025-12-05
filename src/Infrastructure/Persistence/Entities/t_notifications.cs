using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_notifications : AuditableEntity
{
    public required string type { get; set; }
    public required byte[] contact { get; set; }
    public required byte[] message { get; set; }
    public required string status { get; set; }
    public required int retryCount { get; set; }
}
