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

    public async Task<List<OtpReferenceDTO>> GetOtpByOtpAndRefCode(string otpCode, string refCode)
    {
        var items = await this._dbContext.t_otp.Where(o =>
            o.otp == otpCode &&
            o.refCode == refCode &&
            o.isActive &&
            !o.isDeleted &&
            o.expiresAt > DateTime.UtcNow
        ).Select(o => new OtpReferenceDTO
        {
            otpCode = o.otp,
            refCode = o.refCode
        }).ToListAsync();

        return items;
    }
}
