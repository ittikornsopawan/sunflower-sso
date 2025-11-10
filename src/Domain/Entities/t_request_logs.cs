using System;
using Domain.Common;

namespace Domain.Entities;

public class t_request_logs : AuditableEntity
{
    public required string endpoint { get; set; }
    public required string method { get; set; }
    public string requestPayload { get; set; } = default!;
    public string responsePayload { get; set; } = default!;
    public int statusCode { get; set; } = 0;
    public string ipAddress { get; set; } = default!;
    public string userAgent { get; set; } = default!;
    public string durationMs { get; set; } = default!;
}
