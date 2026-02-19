using System;

namespace Infrastructure.Common;

public class AuditableEntity : BaseEntity
{
    /// <summary>
    /// The identifier of the user who created the entity.
    /// </summary>
    public Guid? createdById { get; set; }

    /// <summary>
    /// The date and time when the entity was created.
    /// </summary>
    public DateTime createdAt { get; set; }

    /// <summary>
    /// The identifier of the user who last updated the entity, if applicable.
    /// </summary>
    public Guid? updatedById { get; set; }

    /// <summary>
    /// The date and time when the entity was last updated, if applicable.
    /// </summary>
    public DateTime? updatedAt { get; set; }

    /// <summary>
    /// The status of the entity, which can be used to indicate whether the entity is active, inactive, deleted, or revoked etc.
    /// </summary>
    public string? rowStatus { get; set; }
}
