using System;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.ValueObjects;
using Infrastructure.Common;

namespace Domain.UseCases.Otp.Services;

public interface IOtpService
{
    Task<OtpEntity?> GetExistingOtpByContact(string contact);
    Task<OtpEntity> GenerateOtp(string purpose, string contact);
    Task<OtpEntity?> Verify(string code, string refCode);
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

    /// <summary>
    /// Retrieves an existing OTP entity based on the provided contact information. It converts the contact string into a byte array, queries the repository for an active OTP associated with that contact, and if found, constructs and returns an OtpEntity. If no active OTP is found for the given contact, it returns null.
    /// </summary>
    /// <param name="contact">The contact information (email or mobile number) to retrieve the OTP for.</param>
    /// <returns>The retrieved OtpEntity if found, otherwise null.</returns>
    public async Task<OtpEntity?> GetExistingOtpByContact(string contact)
    {
        var contactOV = new ContactValueObject(contact);
        var existingOtp = await this._otpQueryRepository.GetExistingOtpByContact(contactOV.valueByte);

        if (existingOtp == null) return null;

        var purposeOV = new OtpPurposeValueObject(existingOtp.purpose);
        var codeOV = new OtpCodeValueObject(existingOtp.otpCode);
        var refCodeOV = new OtpRefCodeValueObject(existingOtp.refCode);
        var attemptsOV = new OtpAttemptValueObject(existingOtp.attempts);

        var otpEntity = new OtpEntity(
           contact: contactOV,
           purpose: purposeOV,
           code: codeOV,
           refCode: refCodeOV,
           attempts: attemptsOV
       );

        return otpEntity;
    }

    /// <summary>
    /// Generates a new OTP for the given purpose and contact. It checks for duplicates and ensures that the generated OTP is unique. If the insertion of the OTP entity into the repository fails, it throws an exception.
    /// </summary>
    /// <param name="purpose">Purpose of the OTP (e.g., "login", "reset-password").</param>
    /// <param name="contact">Contact information (email or mobile number).</param>
    /// <returns>The generated OtpEntity.</returns>
    /// <exception cref="ArgumentException">Thrown if purpose or contact is null or empty.</exception>
    /// <exception cref="Exception">Thrown if OTP insertion fails.</exception>
    public async Task<OtpEntity> GenerateOtp(string purpose, string contact)
    {
        if (string.IsNullOrWhiteSpace(purpose))
            throw new ArgumentException("Purpose cannot be empty.", nameof(purpose));

        if (string.IsNullOrWhiteSpace(contact))
            throw new ArgumentException("Contact cannot be empty.", nameof(contact));

        var purposeOV = new OtpPurposeValueObject(purpose);
        var contactOV = new ContactValueObject(contact);
        var codeOV = new OtpCodeValueObject();
        var refCodeOV = new OtpRefCodeValueObject();
        var attemptsOV = new OtpAttemptValueObject(0);

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
            throw new Exception("Failed to insert Otp entity.");

        return otpEntity;
    }

    /// <summary>
    /// Checks for duplicate OTP codes and reference codes in the repository. If a duplicate is found, it generates new codes and checks again recursively until unique codes are generated. This ensures that each OTP code and reference code combination is unique in the system.
    /// </summary>
    /// <param name="code">Otp code value object.</param>
    /// <param name="refCode">Reference Otp code value object.</param>
    /// <returns>A tuple containing unique OtpCodeValueObject and OtpRefCodeValueObject.</returns>
    private async Task<(OtpCodeValueObject code, OtpRefCodeValueObject refCode)> DuplicateOtpCheck(OtpCodeValueObject code, OtpRefCodeValueObject refCode)
    {
        var existingOtp = await this._otpQueryRepository.GetOtpByOtpAndRefCode(code.value, refCode.value);

        if (existingOtp == null || !existingOtp.Any()) return (code, refCode);

        var newCode = new OtpCodeValueObject();
        var newRefCode = new OtpRefCodeValueObject();

        return await DuplicateOtpCheck(newCode, newRefCode);
    }

    /// <summary>
    /// Verifies the provided OTP code against the reference code. It retrieves the existing OTP entity using the reference code, checks if the provided code matches the stored code, and updates the attempt count if verification fails. If verification is successful, it returns the corresponding OtpEntity. If the OTP is not found or verification fails, it throws an exception or returns null accordingly.
    /// </summary>
    /// <param name="code">The OTP code to verify.</param>
    /// <param name="refCode">The reference OTP code.</param>
    /// <returns>The verified OtpEntity if successful, otherwise null.</returns>
    /// <exception cref="ArgumentException">Thrown if code or refCode is null or empty.</exception>
    /// <exception cref="Exception"></exception>
    public async Task<OtpEntity?> Verify(string code, string refCode)
    {
        try
        {

            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("code cannot be empty.", nameof(code));

            if (string.IsNullOrWhiteSpace(refCode))
                throw new ArgumentException("refCode cannot be empty.", nameof(refCode));

            var existingOtp = await this._otpQueryRepository.GetOtpByRefCode(refCode);
            if (existingOtp == null) throw new Exception("OTP not found.");

            var codeOV = new OtpCodeValueObject(existingOtp.otpCode);
            var refCodeOV = new OtpRefCodeValueObject(existingOtp.refCode);

            var otpVerifyEntity = new OtpVerifyEntity(code: codeOV, refCode: refCodeOV);

            otpVerifyEntity.Verify(code);

            var otpResult = OtpResultStaticValue.SUCCESS;

            if (!otpVerifyEntity.isVerified)
            {
                otpResult = OtpResultStaticValue.FAILED;

                await this._otpCommandRepository.IncreaseOtpAttempts(existingOtp.id);
            }

            var otp = await this._otpQueryRepository.GetOtpById(existingOtp.id);
            if (otp == null) throw new Exception("OTP not found after verification.");

            codeOV = new OtpCodeValueObject(otp!.otpCode);
            refCodeOV = new OtpRefCodeValueObject(otp!.refCode);
            var purposeOV = new OtpPurposeValueObject(otp!.purpose);
            var attemptsOV = new OtpAttemptValueObject(otp!.attempts);

            var otpEntity = new OtpEntity(
                id: otp.id,
                purpose: purposeOV,
                code: otpResult == OtpResultStaticValue.SUCCESS ? codeOV : null,
                refCode: refCodeOV,
                attempts: attemptsOV,
                result: otpResult
            );

            await this._otpCommandRepository.UpdateOtp(otpEntity);

            return otpEntity;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in OTP verification: {ex.Message}");
            return null;
        }
    }
}
