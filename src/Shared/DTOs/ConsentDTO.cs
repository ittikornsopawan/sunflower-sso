using System;

namespace Shared.DTOs;

public class ConsentDTO
{
    public Guid id { get; set; }
    public required ConsentTypeDTO consentType { get; set; }
    public required string version { get; set; }
    public required string name { get; set; }
    public string? description { get; set; }
    public required string content { get; set; } = string.Empty;
    public required string language { get; set; }
}

public class ConsentTypeDTO
{
    public Guid id { get; set; }
    public required string name { get; set; }
    public string? description { get; set; }
    public bool isRequired { get; set; } = false;
    public required string latestVersion { get; set; }
}

public class AcceptionConsentDTO
{
    public Guid? userId { get; set; }
    public required Guid consentId { get; set; }
    public required Guid consentTypeId { get; set; }
    public required string version { get; set; }
    public required bool result { get; set; } = false;
}