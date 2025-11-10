using System;
using Domain.Common;

namespace Domain.Entities;

public class t_addresses : AuditableEntity
{
    public DateTimeOffset effectiveAt { get; set; }
    public DateTimeOffset? expiresAt { get; set; }
    public required string type { get; set; }
    public required byte[] address { get; set; }
    public byte[] addressAdditional { get; set; } = default!;
    public required string countryCode { get; set; }
    public string countryName { get; set; } = default!;
    public string state { get; set; } = default!;
    public string city { get; set; } = default!;
    public string district { get; set; } = default!;
    public string subDistrict { get; set; } = default!;
    public string postalCode { get; set; } = default!;
    public string geofenceArea { get; set; } = default!;
    public string geofenceCenter { get; set; } = default!;
    public int? geofenceRadiusMeters { get; set; }
}
