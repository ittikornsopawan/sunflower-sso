using System;

namespace Shared.DTOs;

public class OtpDTO
{
    public Guid? id { get; set; }
    public required string refCode { get; set; }
    public required string otpCode { get; set; }
    public required string purpose { get; set; }
    public int attempts { get; set; }
    public DateTime expiresAt { get; set; }
}

public class OtpReferenceDTO
{
    public required Guid id { get; set; }
    public required string refCode { get; set; }
    public required string otpCode { get; set; }
}

public class OtpVerificationDTO
{
    public required Guid referenceId { get; set; }
}