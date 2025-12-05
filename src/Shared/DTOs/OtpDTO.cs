using System;

namespace Shared.DTOs;

public class OtpDTO
{
    public Guid? id { get; set; }
    public required string refCode { get; set; }
    public DateTime? expiredAt { get; set; }
}

public class OtpReferenceDTO
{
    public required string refCode { get; set; }
    public required string otpCode { get; set; }
}

public class OtpVerificationDTO
{
    public required Guid referenceId { get; set; }
}