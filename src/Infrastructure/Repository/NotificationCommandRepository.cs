using System;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Entities;

namespace Infrastructure.Repository;

public class NotificationCommandRepository : BaseRepository, INotificationCommandRepository
{
    public NotificationCommandRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Guid> CreateNotification(NotificationEntity notificationEntity)
    {
        var notification = new t_notifications
        {
            id = notificationEntity.id,
            type = notificationEntity.type.value,
            contact = notificationEntity.contact.valueByte,
            message = notificationEntity.message.valueByte,
            status = notificationEntity.status.value,
            retryCount = notificationEntity.retryCount.value
        };

        var entity = await this._dbContext.t_notifications.AddAsync(notification);
        await this._dbContext.SaveChangesAsync();

        return entity.Entity.id;
    }
}
