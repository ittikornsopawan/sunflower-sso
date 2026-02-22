using System;

namespace Presentation.Models;

public class OtpResponseModel
{
    public required Guid id { get; set; }
    public required string refCode { get; set; }
    public required DateTime expiresAt { get; set; }
}
