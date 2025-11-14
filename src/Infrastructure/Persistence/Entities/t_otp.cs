using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_otp : AuditableEntity
{
    public DateTime expiresAt { get; set; }
    public required string refCode { get; set; }
    public required string otp { get; set; }
    public int verifyCount { get; set; } = 0;
}
