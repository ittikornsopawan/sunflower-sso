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

    public async Task<Guid> InsertOtp(OtpEntity otpEntity)
    {
        try
        {
            var otp = new t_otp
            {
                id = otpEntity.id,
                code = otpEntity.id.ToString("N").Substring(0, 16),
                purpose = otpEntity.purpose!.value,
                otp = otpEntity.code!.value,
                refCode = otpEntity.refCode!.value,
                attempts = otpEntity.attempts!.value,
                result = otpEntity.result,
                expiresAt = otpEntity.expiry ?? DateTime.UtcNow.AddMinutes(5),
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
            Console.WriteLine("Error inserting Otp: " + ex.Message);
            return Guid.Empty;
        }
    }

    public async Task UpdateOtp(OtpEntity otpEntity)
    {
        try
        {
            var exist = await _dbContext.t_otp.FirstOrDefaultAsync(o => o.id == otpEntity.id);
            if (exist == null) return;

            exist.code = otpEntity.id.ToString("N").Substring(0, 16);
            exist.purpose = otpEntity.purpose!.value;
            exist.otp = otpEntity.code!.value;
            exist.refCode = otpEntity.refCode!.value;
            exist.attempts = otpEntity.attempts!.value;
            exist.result = otpEntity.result;
            exist.expiresAt = otpEntity.expiry ?? DateTime.UtcNow.AddMinutes(5);
            exist.isDeleted = false;
            exist.isActive = true;
            exist.updatedById = Guid.Empty;
            exist.updatedAt = DateTime.UtcNow;

            _dbContext.t_otp.Update(exist);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error updating Otp: " + ex.Message);
        }
    }

    public async Task<int?> IncreaseOtpAttempts(Guid id)
    {
        try
        {
            var exist = await _dbContext.t_otp.FirstOrDefaultAsync(o => o.id == id);
            if (exist != null)
            {
                exist.attempts += 1;
                _dbContext.t_otp.Update(exist);
                await _dbContext.SaveChangesAsync();
            }

            return exist?.attempts ?? 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error increasing Otp attempts: " + ex.Message);
            return null;
        }
    }
}
