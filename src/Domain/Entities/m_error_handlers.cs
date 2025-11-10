using System;
using Domain.Common;

namespace Domain.Entities;

public class m_error_handlers : AuditableEntity
{
    public required string statusCode { get; set; }
    public required string message { get; set; }
    public string language { get; set; } = default!;
}
