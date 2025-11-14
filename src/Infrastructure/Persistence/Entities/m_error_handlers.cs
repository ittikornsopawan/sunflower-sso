using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class m_error_handlers : AuditableEntity
{
    public required string statusCode { get; set; }
    public required string message { get; set; }
    public string language { get; set; } = default!;
}
