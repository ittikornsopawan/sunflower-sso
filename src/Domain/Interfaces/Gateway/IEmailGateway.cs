using System;

namespace Domain.Interfaces.Gateway;

public interface IEmailGateway
{
    Task Send(string to, string subject, string body);
}
