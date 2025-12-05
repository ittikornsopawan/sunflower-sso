using System;
using Infrastructure.Common;


namespace Infrastructure.Persistence.Entities;

public class t_otp_logs : AuditableEntity
{
    public required string otpId { get; set; }
    public int countNo { get; set; }
    public required string purpose { get; set; }
    public string ipAddress { get; set; } = default!;
    public string deviceId { get; set; } = default!;
    public string deviceOs { get; set; } = default!;
    public string locationName { get; set; } = default!;
    public string latitude { get; set; } = default!;
    public string longitude { get; set; } = default!;
    public string geofenceCenter { get; set; } = default!;
    public string geofenceRadiusMeters { get; set; } = default!;
    public required string result { get; set; }
    public string remark { get; set; } = default!;
}
