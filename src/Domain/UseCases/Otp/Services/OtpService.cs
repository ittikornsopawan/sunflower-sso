using System;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.ValueObjects;

namespace Domain.UseCases.Otp.Services;

public interface IOtpService
{
    Task<OTPEntity> GenerateOTP(string purpose, string contact);
}

public class OtpService : IOtpService
{
    private readonly IOtpQueryRepository _otpQueryRepository;
    private readonly IOtpCommandRepository _otpCommandRepository;
    public OtpService(IOtpQueryRepository otpQueryRepository, IOtpCommandRepository otpCommandRepository)
    {
        _otpQueryRepository = otpQueryRepository;
        _otpCommandRepository = otpCommandRepository;
    }

    public async Task<OTPEntity> GenerateOTP(string purpose, string contact)
    {
        if (string.IsNullOrWhiteSpace(purpose))
            throw new ArgumentException("Purpose cannot be empty.", nameof(purpose));

        if (string.IsNullOrWhiteSpace(contact))
            throw new ArgumentException("Contact cannot be empty.", nameof(contact));

        var purposeOV = new OTPPurposeValueObject(purpose);
        var contactOV = new ContactValueObject(contact);
        var codeOV = new OTPCodeValueObject();
        var refCodeOV = new OTPRefCodeValueObject();
        var attemptsOV = new OTPAttemptValueObject(0);

        (codeOV, refCodeOV) = await DuplicateOtpCheck(codeOV, refCodeOV);

        var otpEntity = new OTPEntity(
            contact: contactOV,
            purpose: purposeOV,
            code: codeOV,
            refCode: refCodeOV,
            attempts: attemptsOV
        );

        var insertedId = await this._otpCommandRepository.InsertOtp(otpEntity);
        if (insertedId != otpEntity.id || insertedId == Guid.Empty)
            throw new Exception("Failed to insert OTP entity.");

        return otpEntity;
    }

    private async Task<(OTPCodeValueObject code, OTPRefCodeValueObject refCode)> DuplicateOtpCheck(OTPCodeValueObject code, OTPRefCodeValueObject refCode)
    {
        var existingOtp = await this._otpQueryRepository.GetOtpByOtpAndRefCode(code.value, refCode.value);

        if (existingOtp == null || !existingOtp.Any()) return (code, refCode);

        var newCode = new OTPCodeValueObject();
        var newRefCode = new OTPRefCodeValueObject();

        return await DuplicateOtpCheck(newCode, newRefCode);
    }
}
