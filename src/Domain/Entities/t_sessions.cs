using System;
using Domain.Common;

namespace Domain.Entities;

public class t_sessions : AuditableEntity
{
    public bool isRevoked { get; set; } = false;
    public DateTimeOffset? revokedAt { get; set; } = default!;
    public Guid? revokedBy { get; set; } = default!;
    public string revokedReason { get; set; } = default!;
    public DateTimeOffset expiresAt { get; set; }
    public Guid userId { get; set; }
    public required string accessToken { get; set; }
    public DateTimeOffset accessTokenExpiresAt { get; set; }
    public required string refreshAccessToken { get; set; }
    public DateTimeOffset refreshAccessTokenExpiresAt { get; set; }
    public required string payload { get; set; }
}
