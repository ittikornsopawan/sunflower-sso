using System;
using Domain.Interfaces.Repository;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;

namespace Infrastructure.Repository;

public class OtpQueryRepository : BaseRepository, IOtpQueryRepository
{
    public OtpQueryRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<OtpDTO?> GetOtpById(Guid id)
    {
        var item = await this._dbContext.t_otp.Where(o =>
            o.id == id &&
            o.rowStatus == "ACTIVE" &&
            o.expiresAt > DateTime.UtcNow
        ).FirstOrDefaultAsync();

        if (item == null) return null;

        return new OtpDTO
        {
            id = item.id,
            refCode = item.refCode,
            otpCode = item.otp,
            purpose = item.purpose,
            attempts = item.attempts,
            expiresAt = item.expiresAt
        };
    }

    public async Task<List<OtpReferenceDTO>> GetOtpByOtpAndRefCode(string otpCode, string refCode)
    {
        var items = await this._dbContext.t_otp.Where(o =>
            o.otp == otpCode &&
            o.refCode == refCode &&
            o.rowStatus == "ACTIVE" &&
            o.expiresAt > DateTime.UtcNow
        ).Select(o => new OtpReferenceDTO
        {
            id = o.id,
            otpCode = o.otp,
            refCode = o.refCode
        }).ToListAsync();

        return items;
    }

    public async Task<OtpReferenceDTO?> GetOtpByRefCode(string refCode)
    {
        var item = await this._dbContext.t_otp
            .Where(o =>
                o.refCode == refCode &&
                o.rowStatus == "ACTIVE" &&
                o.expiresAt > DateTime.UtcNow
            )
            .Select(o => new OtpReferenceDTO
            {
                id = o.id,
                otpCode = o.otp,
                refCode = o.refCode
            })
            .FirstOrDefaultAsync();

        return item;
    }
}
