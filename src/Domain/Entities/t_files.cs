using System;
using Domain.Common;

namespace Domain.Entities;

public class t_files : AuditableEntity
{
    public DateTimeOffset effectiveAt { get; set; }
    public DateTimeOffset? expiresAt { get; set; }
    public string usageType { get; set; } = default!;
    public string filePath { get; set; } = default!;
    public string fileName { get; set; } = default!;
    public int? fileSize { get; set; } = default!;
    public string fileSizeUnit { get; set; } = default!;
    public string fileDimension { get; set; } = default!;
    public string fileExtension { get; set; } = default!;
    public string mimeType { get; set; } = default!;
    public string description { get; set; } = default!;
    public string storageProvider { get; set; } = default!;
    public string storageBucket { get; set; } = default!;
    public string storageKey { get; set; } = default!;
}
