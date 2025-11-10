using System;
using Domain.Common;

namespace Domain.Entities;

public class t_user_open_authentication : AuditableEntity
{
    public required string provider { get; set; }
    public string providerName { get; set; } = default!;
    public required string providerUserId { get; set; }
    public Guid userId { get; set; }
    public string email { get; set; } = default!;
    public string displayName { get; set; } = default!;
    public string profilePictureUrl { get; set; } = default!;
    public string accessToken { get; set; } = default!;
    public string refreshToken { get; set; } = default!;
    public DateTimeOffset? tokenExpiresAt { get; set; }
    public string rawResponse { get; set; } = default!;
}
