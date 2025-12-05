using System;

namespace Domain.UseCases.Notification.Services;

public interface INotificationService
{
    Task SendEmail(string contact, string message);
}

public class NotificationService : INotificationService
{
    public NotificationService()
    {

    }

    public async Task SendEmail(string contact, string message)
    {

        await Task.CompletedTask;
    }
}
