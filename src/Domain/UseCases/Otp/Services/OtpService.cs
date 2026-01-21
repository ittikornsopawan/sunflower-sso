using System;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.ValueObjects;

namespace Domain.UseCases.Otp.Services;

public interface IOtpService
{
    Task<OtpEntity> GenerateOTP(string purpose, string contact);
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

    public async Task<OtpEntity> GenerateOTP(string purpose, string contact)
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

        var otpEntity = new OtpEntity(
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

    public async Task Verify(string code, string refCode, Guid? id = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("code cannot be empty.", nameof(code));

        if (string.IsNullOrWhiteSpace(refCode))
            throw new ArgumentException("refCode cannot be empty.", nameof(refCode));

        var codeOV = new OTPCodeValueObject(code);
        var refCodeOV = new OTPRefCodeValueObject(refCode);

        var otpEntity = new OtpEntity(code: codeOV, refCode: refCodeOV);

        // var insertedId = await this._otpCommandRepository.InsertOtp(otpEntity);
    }
}
