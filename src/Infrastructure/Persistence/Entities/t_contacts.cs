using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_contacts : AuditableEntity
{
    public required string channel { get; set; }
    public required string contact { get; set; }
    public required string contactName { get; set; }
    public string available { get; set; } = default!;
    public string remark { get; set; } = default!;
}
