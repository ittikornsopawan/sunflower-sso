using System;
using Shared.DTOs;

namespace Domain.Interfaces.Repository;

public interface IOtpQueryRepository
{
    Task<OtpDTO?> GetOtpById(Guid id);
    Task<List<OtpReferenceDTO>> GetOtpByOtpAndRefCode(string otpCode, string refCode);
    Task<OtpReferenceDTO?> GetOtpByRefCode(string refCode);
}
