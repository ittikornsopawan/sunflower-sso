using System;
using Domain.Entities;
using Domain.Interfaces.Gateway;
using Domain.Interfaces.Repository;
using Domain.ValueObjects;

namespace Domain.UseCases.Notification.Services;

public interface INotificationService
{
    Task<bool> Send(string contact, string message, string subject = "");
}

public class NotificationService : INotificationService
{
    private readonly INotificationCommandRepository _notificationCommandRepository;
    private readonly IEmailGateway _emailGateway;

    public NotificationService(INotificationCommandRepository notificationCommandRepository, IEmailGateway emailGateway)
    {
        _notificationCommandRepository = notificationCommandRepository;
        _emailGateway = emailGateway;
    }

    public async Task<bool> Send(string contact, string message, string subject = "")
    {
        try
        {
            var contactVO = new ContactValueObject(contact);
            var messageVO = new MessageValueObject(message);
            var typeVO = new TypeValueObject(TypeContext.Notification, contactVO.Type());
            var statusVO = new StatusValueObject("PENDING");
            var retryCountVO = new RetryCountValueObject(0);
            var subjectOV = new TitleValueObject(subject);

            var entity = new NotificationEntity(
                contact: contactVO,
                subject: subjectOV,
                type: typeVO,
                message: messageVO,
                status: statusVO,
                retryCount: retryCountVO
            );

            switch (contactVO.Type())
            {
                case "EMAIL":
                    await _emailGateway.Send(contactVO.value, subjectOV.value!, messageVO.value);
                    break;
                case "MOBILE":
                    break;
                default:
                    throw new ArgumentException("Invalid contact type.");
            }
            await _notificationCommandRepository.CreateNotification(entity);

            return false;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("", ex);
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("", ex);
            return false;
        }
    }
}
