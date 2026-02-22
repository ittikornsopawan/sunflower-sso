using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Entity representing a One-Time Password (Otp).
/// </summary>
/// <author>Ittikorn Sopawan</author>
public class OtpEntity : BaseEntity
{
    /// <summary>
    /// The contact (Email or Mobile) associated with this Otp.
    /// </summary>
    public ContactValueObject? contact { get; private set; }

    /// <summary>
    /// The purpose of this Otp.
    /// </summary>
    public OtpPurposeValueObject? purpose { get; private set; }

    /// <summary>
    /// The Referebce Otp code generated.
    /// </summary>
    public OtpRefCodeValueObject? refCode { get; private set; }

    /// <summary>
    /// The Otp code generated.
    /// </summary>
    public OtpCodeValueObject? code { get; private set; }

    /// <summary>
    /// Expiration time of the Otp.
    /// </summary>
    public DateTime? expiry { get; private set; }

    /// <summary>
    /// Number of Verifications.
    /// </summary>
    public OtpAttemptValueObject? attempts { get; private set; }

    /// <summary>
    /// Result Otp verification.
    /// </summary>
    public string result { get; private set; } = "PENDING";

    /// <summary>
    /// Creates a new Otp entity with the specified contact and purpose.
    /// Otp code and expiry will be set later.
    /// </summary>
    /// <param name="contact">The contact value object (email or mobile).</param>
    /// <param name="purpose">The purpose value object.</param>
    /// <param name="attempts">The attempts value object.</param>
    /// <exception cref="ArgumentNullException">Thrown if contact or purpose is null.</exception>
    public OtpEntity(
        ContactValueObject contact,
        OtpPurposeValueObject purpose,
        OtpAttemptValueObject? attempts = null
    )
    {
        this.id = Guid.NewGuid();
        this.contact = contact ?? throw new ArgumentNullException(nameof(contact));
        this.purpose = purpose ?? throw new ArgumentNullException(nameof(purpose));
        this.attempts = attempts ?? new OtpAttemptValueObject(0);
    }

    /// <summary>
    /// Constructor for Otp entity with code and reference code.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="refCode"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public OtpEntity(
        OtpCodeValueObject code,
        OtpRefCodeValueObject refCode
    )
    {
        this.code = code ?? throw new ArgumentNullException(nameof(code));
        this.refCode = refCode ?? throw new ArgumentNullException(nameof(refCode));
    }

    /// <summary>
    /// Constructor for Otp entity.
    /// </summary>
    /// <param name="contact">Contact value object (email or mobile)</param>
    /// <param name="purpose">Purpose value object</param>
    /// <param name="code">Generated Otp code</param>
    /// <param name="refCode">Generated reference Otp code</param>
    /// <param name="expiry">Expiration time of the Otp</param>
    /// <param name="attempts">Number of verification attempts</param>
    /// <author>Ittikorn Sopawan</author>
    public OtpEntity(
        ContactValueObject contact,
        OtpPurposeValueObject purpose,
        OtpCodeValueObject code,
        OtpRefCodeValueObject refCode,
        DateTime? expiry = null,
        OtpAttemptValueObject? attempts = null
    )
    {
        this.id = Guid.NewGuid();
        this.contact = contact ?? throw new ArgumentNullException(nameof(contact));
        this.purpose = purpose ?? throw new ArgumentNullException(nameof(purpose));
        this.code = code ?? throw new ArgumentNullException(nameof(code));
        this.refCode = refCode ?? throw new ArgumentNullException(nameof(refCode));
        this.attempts = attempts ?? throw new ArgumentNullException(nameof(attempts));
        this.expiry = expiry ?? DateTime.UtcNow.AddMinutes(5);
    }

    public OtpEntity(
        OtpPurposeValueObject purpose,
        OtpCodeValueObject code,
        OtpRefCodeValueObject refCode,
        Guid? id = null,
        DateTime? expiry = null,
        OtpAttemptValueObject? attempts = null
    )
    {
        this.id = id ?? Guid.NewGuid();
        this.purpose = purpose ?? throw new ArgumentNullException(nameof(purpose));
        this.code = code ?? throw new ArgumentNullException(nameof(code));
        this.refCode = refCode ?? throw new ArgumentNullException(nameof(refCode));
        this.attempts = attempts ?? throw new ArgumentNullException(nameof(attempts));
        this.expiry = expiry ?? DateTime.UtcNow.AddMinutes(5);
    }

    public OtpEntity(
        OtpPurposeValueObject purpose,
        OtpRefCodeValueObject refCode,
        OtpCodeValueObject? code,
        Guid? id = null,
        DateTime? expiry = null,
        OtpAttemptValueObject? attempts = null,
        string? result = null
    )
    {
        this.id = id ?? Guid.NewGuid();
        this.purpose = purpose ?? throw new ArgumentNullException(nameof(purpose));
        this.code = code;
        this.refCode = refCode ?? throw new ArgumentNullException(nameof(refCode));
        this.attempts = attempts ?? throw new ArgumentNullException(nameof(attempts));
        this.expiry = expiry ?? DateTime.UtcNow.AddMinutes(5);
        this.result = result ?? "PENDING";
    }

    /// <summary>
    /// Sets Otp code, reference code, and expiry.
    /// </summary>
    /// <param name="code">The Otp code.</param>
    /// <param name="refCode">The reference Otp code.</param>
    /// <param name="expiryMinutes">Minutes until expiration.</param>
    public void SetOtp(OtpCodeValueObject code, OtpRefCodeValueObject refCode, int expiryMinutes = 5)
    {
        if (code == null) throw new ArgumentException("Otp code cannot be empty.", nameof(code));

        if (refCode == null) throw new ArgumentException("Reference Otp code cannot be empty.", nameof(refCode));

        this.code = code;
        this.refCode = refCode;
        this.expiry = DateTime.UtcNow.AddMinutes(expiryMinutes);
    }

    /// <summary>
    /// Checks if the Otp is still valid.
    /// </summary>
    /// <returns>True if current time is before the expiry; otherwise, false.</returns>
    public bool IsValid() => this.code != null && DateTime.UtcNow <= this.expiry;

    /// <summary>
    /// Verifies whether the provided Otp code matches the current code and is still valid.
    /// </summary>
    /// <param name="inputCode">The Otp code provided by the user.</param>
    /// <returns>True if the code matches and is still valid; otherwise, false.</returns>
    public bool VerifyOtp(OtpCodeValueObject code)
    {
        if (this.code == null) return false; // no Otp code set

        if (code == null || string.IsNullOrWhiteSpace(code.value)) return false; // empty input is invalid
        return IsValid() && code.value == this.code.value;
    }
}
