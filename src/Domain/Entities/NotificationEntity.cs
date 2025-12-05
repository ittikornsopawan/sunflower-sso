using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Entity representing a Notification message to be delivered.
/// </summary>
/// <author>Ittikorn Sopawan</author>
public class NotificationEntity : BaseEntity
{
    /// <summary>
    /// The notification type (EMAIL, SMS, PUSH).
    /// </summary>
    public TypeValueObject type { get; private set; }

    /// <summary>
    /// The target contact (email or mobile), stored as byte[].
    /// </summary>
    public ContactValueObject contact { get; private set; }

    /// <summary>
    /// The notification message content, stored as byte[].
    /// </summary>
    public MessageValueObject message { get; private set; }

    /// <summary>
    /// The current delivery status of the notification.
    /// </summary>
    public StatusValueObject status { get; private set; }

    /// <summary>
    /// Number of retry attempts made for delivering the notification.
    /// </summary>
    public RetryCountValueObject retryCount { get; private set; }

    /// <summary>
    /// Constructor for Notification entity.
    /// </summary>
    /// <param name="contact">Contact value object (email or mobile, stored as byte[])</param>
    /// <param name="type">Notification type value object (EMAIL / SMS / PUSH)</param>
    /// <param name="message">Message value object (byte[])</param>
    /// <param name="status">Notification status value object, defaults to PENDING</param>
    /// <param name="retryCount">Retry counter value object, defaults to 0</param>
    /// <exception cref="ArgumentNullException">Thrown when contact, type, or message is null.</exception>
    /// <author>Ittikorn Sopawan</author>
    public NotificationEntity(
        ContactValueObject contact,
        TypeValueObject type,
        MessageValueObject message,
        StatusValueObject? status = null,
        RetryCountValueObject? retryCount = null)
    {
        this.id = Guid.NewGuid();
        this.contact = contact ?? throw new ArgumentNullException(nameof(contact));
        this.type = type ?? throw new ArgumentNullException(nameof(type));
        this.message = message ?? throw new ArgumentNullException(nameof(message));
        this.status = status ?? new StatusValueObject("PENDING");
        this.retryCount = retryCount ?? new RetryCountValueObject(0);
    }
}