using System;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;

namespace Infrastructure.Repository;

public class OtpCommandRepository : BaseRepository, IOtpCommandRepository
{
    public OtpCommandRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Guid> InsertOtp(OTPEntity otpEntity)
    {
        try
        {
            var otp = new t_otp
            {
                id = otpEntity.id,
                code = otpEntity.id.ToString("N").Substring(0, 16),
                purpose = otpEntity.purpose.value,
                otp = otpEntity.code!.value,
                refCode = otpEntity.refCode!.value,
                attempts = otpEntity.attempts.value,
                result = otpEntity.result,
                expiresAt = otpEntity.expiry,
                isDeleted = false,
                isActive = true,
                createdById = Guid.Empty,
                createdAt = DateTime.UtcNow
            };

            var entity = await _dbContext.t_otp.AddAsync(otp);
            await _dbContext.SaveChangesAsync();
            return entity.Entity.id;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error inserting OTP: ", ex.Message);
            return Guid.Empty;
        }
    }
}
