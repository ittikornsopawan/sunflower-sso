using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_user_authentications : AuditableEntity
{
    public DateTime effectiveAt { get; set; }
    public DateTime? expiresAt { get; set; }
    public Guid userId { get; set; }
    public bool isTemporary { get; set; } = false;
    public Guid algorithmId { get; set; }
    public required string algorithmKeys { get; set; }
    public required byte[] passwordHash { get; set; }
}
