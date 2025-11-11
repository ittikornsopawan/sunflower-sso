using System;
using Domain.Common;

namespace Domain.Entities;

public class t_sessions : AuditableEntity
{
    public bool isRevoked { get; set; } = false;
    public DateTime? revokedAt { get; set; } = default!;
    public Guid? revokedBy { get; set; } = default!;
    public string revokedReason { get; set; } = default!;
    public DateTime expiresAt { get; set; }
    public Guid userId { get; set; }
    public required string accessToken { get; set; }
    public DateTime accessTokenExpiresAt { get; set; }
    public required string refreshAccessToken { get; set; }
    public DateTime refreshAccessTokenExpiresAt { get; set; }
    public required string payload { get; set; }
}
