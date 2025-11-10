using System;
using Domain.Common;

namespace Domain.Entities;

public class m_attributes : AuditableEntity
{
    public bool isParameter { get; set; }
    public bool isRequired { get; set; }
    public bool isDisplay { get; set; }
    public string category { get; set; } = default!;
    public string keyGroup { get; set; } = default!;
    public required string name { get; set; }
    public required string key { get; set; }
    public required string dataType { get; set; }
    public string title { get; set; } = default!;
    public string description { get; set; } = default!;
}
