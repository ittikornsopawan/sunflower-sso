using System;

namespace Presentation.Models.Authentication;

/// <summary>
/// Base model for any request that supports file uploads.
/// </summary>
public abstract class BaseModel
{
    /// <summary>
    /// A list of files included in the request.
    /// </summary>
    public List<FileModel>? files { get; set; }
}

/// <summary>
/// Represents an uploaded file with business-type classification.
/// </summary>
public class FileModel
{
    /// <summary>
    /// Raw file object uploaded from the client.
    /// </summary>
    public required IFormFile file { get; set; }

    /// <summary>
    /// File business type such as PROFILE, KYC_ID_CARD, ATTACHMENT.
    /// </summary>
    public required string fileType { get; set; }
}