using System;
using Shared.DTOs;

namespace Domain.Interfaces.Repository;

public interface INotificationQueryRepository
{
    Task<List<NotificationDTO>> GetNotificationTemplates(Guid? id = null, string? key = null);
}
