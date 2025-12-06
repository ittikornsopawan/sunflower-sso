using System;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;

namespace Infrastructure.Repository;

public class NotificationQueryRepository : BaseRepository, INotificationQueryRepository
{
    public NotificationQueryRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<NotificationDTO>> GetNotificationTemplates(Guid? id = null, string? key = null)
    {
        var query = this._dbContext.t_notification_templates.AsQueryable();

        if (id != null && id != Guid.Empty && id.HasValue)
            query = query.Where(x => x.id == id.Value);

        if (!string.IsNullOrEmpty(key))
            query = query.Where(x => x.key == key);

        var result = await query.ToListAsync();
        var notificationTemplates = result.Select(x => new NotificationDTO
        {
            key = x.key,
            version = x.version,
            name = x.name,
            type = x.type,
            subject = x.subject,
            isHtml = x.isHtml,
            content = x.content,
            variables = x.variables
        }).ToList();

        return notificationTemplates;
    }
}
