using System;

namespace Infrastructure.Common;

public static class EntityStatusStaticValue
{
    public const string ACTIVE = "ACTIVE";
    public const string INACTIVE = "INACTIVE";
    public const string DELETED = "DELETED";
    public const string REVOKED = "REVOKED";
}

public static class OtpResultStaticValue
{
    public const string PENDING = "PENDING";
    public const string SUCCESS = "SUCCESS";
    public const string FAILED = "FAILED";
    public const string EXPIRED = "EXPIRED";
}