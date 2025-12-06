using System;

namespace Shared.DTOs;

public class NotificationDTO
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
