using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Entity representing a One-Time Password (OTP).
/// </summary>
/// <author>Ittikorn Sopawan</author>
public class OTPEntity : BaseEntity
{
    /// <summary>
    /// The contact (Email or Mobile) associated with this OTP.
    /// </summary>
    public ContactValueObject contact { get; private set; }

    /// <summary>
    /// The purpose of this OTP.
    /// </summary>
    public OTPPurposeValueObject purpose { get; private set; }

    /// <summary>
    /// The Referebce OTP code generated.
    /// </summary>
    public OTPRefCodeValueObject? refCode { get; private set; }

    /// <summary>
    /// The OTP code generated.
    /// </summary>
    public OTPCodeValueObject? code { get; private set; }

    /// <summary>
    /// Expiration time of the OTP.
    /// </summary>
    public DateTime expiry { get; private set; }

    /// <summary>
    /// Number of Verifications.
    /// </summary>
    public OTPAttemptValueObject attempts { get; private set; }

    /// <summary>
    /// Result OTP verification.
    /// </summary>
    public string result { get; private set; } = "PENDING";


    /// <summary>
    /// Constructor for OTP entity.
    /// </summary>
    /// <param name="contact">Contact value object (email or mobile)</param>
    /// <param name="purpose">Purpose value object</param>
    /// <param name="code">Generated OTP code</param>
    /// <param name="expiry">Expiration time of the OTP</param>
    /// <author>Ittikorn Sopawan</author>
    public OTPEntity(ContactValueObject contact, OTPPurposeValueObject purpose, OTPCodeValueObject code, OTPRefCodeValueObject refCode, DateTime? expiry = null, OTPAttemptValueObject? attempts = null)
    {
        this.id = Guid.NewGuid();
        this.contact = contact ?? throw new ArgumentNullException(nameof(contact));
        this.purpose = purpose ?? throw new ArgumentNullException(nameof(purpose));
        this.code = code ?? throw new ArgumentNullException(nameof(code));
        this.refCode = refCode ?? throw new ArgumentNullException(nameof(refCode));
        this.attempts = attempts ?? throw new ArgumentNullException(nameof(attempts));
        this.expiry = expiry ?? DateTime.UtcNow.AddMinutes(5);
    }

    /// <summary>
    /// Creates a new OTP entity with the specified contact and purpose.
    /// OTP code and expiry will be set later.
    /// </summary>
    /// <param name="contact">The contact value object (email or mobile).</param>
    /// <param name="purpose">The purpose value object.</param>
    /// <exception cref="ArgumentNullException">Thrown if contact or purpose is null.</exception>
    public OTPEntity(ContactValueObject contact, OTPPurposeValueObject purpose, OTPAttemptValueObject? attempts = null)
    {
        this.id = Guid.NewGuid();
        this.contact = contact ?? throw new ArgumentNullException(nameof(contact));
        this.purpose = purpose ?? throw new ArgumentNullException(nameof(purpose));
        this.attempts = new OTPAttemptValueObject(0);
    }

    /// <summary>
    /// Sets OTP code, reference code, and expiry.
    /// </summary>
    /// <param name="code">The OTP code.</param>
    /// <param name="refCode">The reference OTP code.</param>
    /// <param name="expiryMinutes">Minutes until expiration.</param>
    public void SetOTP(OTPCodeValueObject code, OTPRefCodeValueObject refCode, int expiryMinutes = 5)
    {
        if (code == null) throw new ArgumentException("OTP code cannot be empty.", nameof(code));

        if (refCode == null) throw new ArgumentException("Reference OTP code cannot be empty.", nameof(refCode));

        this.code = code;
        this.refCode = refCode;
        this.expiry = DateTime.UtcNow.AddMinutes(expiryMinutes);
    }

    /// <summary>
    /// Checks if the OTP is still valid.
    /// </summary>
    /// <returns>True if current time is before the expiry; otherwise, false.</returns>
    public bool IsValid() => this.code != null && DateTime.UtcNow <= this.expiry;

    /// <summary>
    /// Verifies whether the provided OTP code matches the current code and is still valid.
    /// </summary>
    /// <param name="inputCode">The OTP code provided by the user.</param>
    /// <returns>True if the code matches and is still valid; otherwise, false.</returns>
    public bool VerifyOTP(OTPCodeValueObject code)
    {

        if (this.code == null) return false; // no OTP code set

        if (code == null || string.IsNullOrWhiteSpace(code.value)) return false; // empty input is invalid

        // Check if OTP is still valid and matches the current code
        return IsValid() && code.value == this.code.value;
    }
}
