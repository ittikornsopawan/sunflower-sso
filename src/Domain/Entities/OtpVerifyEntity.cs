using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class OtpVerifyEntity : BaseEntity
{
    public OtpRefCodeValueObject refCode { get; private set; }
    public OtpCodeValueObject code { get; private set; }

    public OtpVerifyEntity(OtpRefCodeValueObject refCode, OtpCodeValueObject code)
    {
        this.code = code ?? throw new ArgumentNullException(nameof(code));
        this.refCode = refCode ?? throw new ArgumentNullException(nameof(refCode));
    }
}
