using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_otp : AuditableEntity
{
    public DateTime expiresAt { get; set; }
    public required string purpose { get; set; }
    public required byte[] contact { get; set; }
    public required string refCode { get; set; }
    public required string otp { get; set; }
    public int attempts { get; set; } = 0;
    public required string result { get; set; }
}
