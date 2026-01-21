using System;

namespace Presentation.Models;

public class OtpVerifyRequestModel
{
    public required string refCode { get; set; }
    public required string code { get; set; }

}
