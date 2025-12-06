using System;
using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface INotificationCommandRepository
{
    Task<Guid> CreateNotification(NotificationEntity notificationEntity);
}
