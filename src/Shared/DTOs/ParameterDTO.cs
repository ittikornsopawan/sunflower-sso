using System;

namespace Shared.DTOs;

public class ParameterDTO
{
    public string? category { get; set; }
    public required string key { get; set; }
    public string? title { get; set; }
    public string? description { get; set; }
    public string? language { get; set; }
    public string? value { get; set; }
}
