using System;

namespace Presentation.Models;

public class OtpVerifyModel
{
    public required string otpRefCode { get; set; }
    public required string otpCode { get; set; }
    public string? referenceCode { get; set; }
}
