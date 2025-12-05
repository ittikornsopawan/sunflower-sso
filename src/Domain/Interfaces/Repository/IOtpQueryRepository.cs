using System;
using Shared.DTOs;

namespace Domain.Interfaces.Repository;

public interface IOtpQueryRepository
{
    Task<List<OtpReferenceDTO>> GetOtpByOtpAndRefCode(string otpCode, string refCode);
}
