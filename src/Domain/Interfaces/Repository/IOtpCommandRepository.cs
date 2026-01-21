using System;
using Domain.Entities;
using Shared.DTOs;

namespace Domain.Interfaces.Repository;

public interface IOtpCommandRepository
{
    Task<Guid> InsertOtp(OtpEntity otpEntity);
}
