using System;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Entities;

public class t_notification_templates : AuditableEntity
{
    public required string key { get; set; }
    public required string version { get; set; }
    public required string name { get; set; }
    public required string type { get; set; }
    public string? subject { get; set; }
    public bool isHtml { get; set; } = false;
    public required string content { get; set; }
    public string? variables { get; set; }
}
