using System;
using Domain.Common;

namespace Domain.Entities;

public class t_otp : AuditableEntity
{
    public DateTimeOffset expiresAt { get; set; }
    public required string refCode { get; set; }
    public required string otp { get; set; }
    public int verifyCount { get; set; } = 0;
}
