namespace Domain.ValueObjects;

/// <summary>
/// Generic Audit ValueObject.
/// Represents who performed an action, when, action type (actor type), and an optional boolean flag.
/// Can be used for Created, Updated, Deleted, Active, or any custom entity status tracking.
/// </summary>
/// <remarks>
/// Author: Ittikorn Sopawan
/// </remarks>
public sealed record AuditStatusValueObject
{
    /// <summary>
    /// The user who performed the action.
    /// </summary>
    public Guid actionById { get; }

    /// <summary>
    /// The timestamp when the action was performed.
    /// </summary>
    public DateTime actionAt { get; }

    /// <summary>
    /// The type of action performed (e.g., Created, Updated, Deleted).
    /// </summary>
    public string actionType { get; }

    /// <summary>
    /// Optional boolean flag for entity status or other purposes.
    /// </summary>
    public bool? flag { get; }

    /// <summary>
    /// Constructor using Guid.
    /// </summary>
    /// <param name="actionById">The user who performed the action.</param>
    /// <param name="actionAt">The timestamp of the action.</param>
    /// <param name="actionType">The type of action performed.</param>
    /// <param name="flag">Optional boolean flag.</param>
    /// <exception cref="ArgumentException">Thrown when parameters are invalid.</exception>
    /// <author>Ittikorn Sopawan</author>
    public AuditStatusValueObject(Guid actionById, DateTime actionAt, string actionType, bool? flag = null)
    {
        if (actionById == Guid.Empty)
            throw new ArgumentException("actionById cannot be empty GUID.", nameof(actionById));

        if (actionAt == default)
            throw new ArgumentException("actionAt cannot be default DateTime.", nameof(actionAt));

        if (string.IsNullOrWhiteSpace(actionType))
            throw new ArgumentException("actionType cannot be null or empty.", nameof(actionType));

        this.actionById = actionById;
        this.actionAt = actionAt;
        this.actionType = actionType;
        this.flag = flag;
    }

    /// <summary>
    /// Constructor using string actionById.
    /// </summary>
    /// <param name="actionById">The user who performed the action (as string GUID).</param>
    /// <param name="actionAt">The timestamp of the action.</param>
    /// <param name="actionType">The type of action performed.</param>
    /// <param name="flag">Optional boolean flag.</param>
    /// <exception cref="ArgumentException">Thrown when parameters are invalid.</exception>
    /// <author>Ittikorn Sopawan</author>
    public AuditStatusValueObject(string actionById, DateTime actionAt, string actionType, bool? flag = null) : this(Guid.Parse(actionById), actionAt, actionType, flag)
    {

    }

    /// <summary>
    /// Returns a string representation of the AuditStatusValueObject.
    /// </summary>
    /// <returns>String describing the audit action.</returns>
    /// <author>Ittikorn Sopawan</author>
    public override string ToString() => $"actionById: {actionById}, actionAt: {actionAt:O}, actionType: {actionType}, flag: {flag}";

    /// <summary>
    /// Creates a Created audit action.
    /// </summary>
    /// <param name="userId">The user who performed the action.</param>
    /// <param name="flag">Optional boolean flag.</param>
    /// <returns>AuditStatusValueObject representing a Created action.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static AuditStatusValueObject Created(Guid userId, bool? flag = null) => new AuditStatusValueObject(userId, DateTime.UtcNow, "Created", flag);

    /// <summary>
    /// Creates an Updated audit action.
    /// </summary>
    /// <param name="userId">The user who performed the action.</param>
    /// <param name="flag">Optional boolean flag.</param>
    /// <returns>AuditStatusValueObject representing an Updated action.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static AuditStatusValueObject Updated(Guid userId, bool? flag = null) => new AuditStatusValueObject(userId, DateTime.UtcNow, "Updated", flag);

    /// <summary>
    /// Creates a Deleted audit action.
    /// </summary>
    /// <param name="userId">The user who performed the action.</param>
    /// <param name="flag">Optional boolean flag.</param>
    /// <returns>AuditStatusValueObject representing a Deleted action.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static AuditStatusValueObject Deleted(Guid userId, bool? flag = null) => new AuditStatusValueObject(userId, DateTime.UtcNow, "Deleted", flag);

    /// <summary>
    /// Creates an Active audit action.
    /// </summary>
    /// <param name="userId">The user who performed the action.</param>
    /// <param name="flag">Optional boolean flag.</param>
    /// <returns>AuditStatusValueObject representing an Active action.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static AuditStatusValueObject Active(Guid userId, bool? flag = null) => new AuditStatusValueObject(userId, DateTime.UtcNow, "Active", flag);

    /// <summary>
    /// Creates a custom audit action.
    /// </summary>
    /// <param name="userId">The user who performed the action.</param>
    /// <param name="actionType">Custom action type.</param>
    /// <param name="flag">Optional boolean flag.</param>
    /// <returns>AuditStatusValueObject representing a custom action.</returns>
    /// <exception cref="ArgumentException">Thrown when actionType is null or empty.</exception>
    /// <author>Ittikorn Sopawan</author>
    public static AuditStatusValueObject Custom(Guid userId, string actionType, bool? flag = null) => new AuditStatusValueObject(userId, DateTime.UtcNow, actionType, flag);
}