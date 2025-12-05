using System;
using Shared.DTOs;

namespace Presentation.Models;

public class RegisterModel
{
    public UserDTO? user { get; set; }
    public UserAuthenticationDTO? authentication { get; set; }
    public List<AddressDTO>? address { get; set; }
    public List<ContactDTO>? contact { get; set; }
    public List<AcceptionConsentDTO>? consents { get; set; }
    public OtpVerificationDTO? otp { get; set; }
}