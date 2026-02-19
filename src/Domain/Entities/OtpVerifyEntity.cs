using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class OtpVerifyEntity : BaseEntity
{
    public OtpRefCodeValueObject refCode { get; private set; }
    public OtpCodeValueObject code { get; private set; }
    public bool isVerified { get; private set; } = false;

    public OtpVerifyEntity(OtpRefCodeValueObject refCode, OtpCodeValueObject code)
    {
        this.code = code ?? throw new ArgumentNullException(nameof(code));
        this.refCode = refCode ?? throw new ArgumentNullException(nameof(refCode));
    }

    public void Verify(string code) => isVerified = this.code.value == code;
}
