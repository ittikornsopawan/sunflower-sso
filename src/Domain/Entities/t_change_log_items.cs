using Domain.Common;

namespace Domain.Entities;

public class t_change_log_items : AuditableEntity
{
    public Guid headerId { get; set; }
    public required string columnName { get; set; }
    public required string oldValue { get; set; }
    public required string newValue { get; set; }
}