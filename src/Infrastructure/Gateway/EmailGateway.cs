using System;
using System.Net;
using System.Net.Mail;
using Domain.Interfaces.Gateway;
using Microsoft.Extensions.Options;
using Shared.Configurations;

namespace Infrastructure.Gateway;

/// <summary>
/// Infrastructure Email Gateway for sending emails via SMTP.
/// Supports Gmail, Outlook, or any SMTP provider configured in appsettings.
/// </summary>
/// <author>Ittikorn Sopawan</author>
public class EmailGateway : IEmailGateway
{
    private readonly AppSettings _appSettings;

    public EmailGateway(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    /// <summary>
    /// Sends an email using the configured SMTP provider, with automatic retry up to 3 attempts.
    /// </summary>
    /// <param name="to">Recipient email address.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="body">Email body (HTML supported).</param>
    /// <exception cref="SmtpException">Thrown if sending fails after all retry attempts.</exception>
    /// <author>Ittikorn Sopawan</author>
    public async Task Send(string to, string subject, string body)
    {
        int maxRetries = _appSettings.EmailSettings.RetryAttempt;
        int attempt = 0;
        Exception? lastException = null;

        while (attempt < maxRetries)
        {
            try
            {
                using var client = new SmtpClient(_appSettings.EmailSettings.Host, _appSettings.EmailSettings.Port)
                {
                    Credentials = new NetworkCredential(
                        _appSettings.EmailSettings.Username,
                        _appSettings.EmailSettings.Password),
                    EnableSsl = _appSettings.EmailSettings.EnableSsl
                };

                using var mail = new MailMessage()
                {
                    From = new MailAddress(_appSettings.EmailSettings.From),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mail.To.Add(to);

                await client.SendMailAsync(mail);
                return;
            }
            catch (Exception ex)
            {
                lastException = ex;
                attempt++;

                if (attempt >= maxRetries)
                    throw new SmtpException(
                        $"Failed to send email after {maxRetries} attempts.",
                        lastException);

                await Task.Delay(_appSettings.EmailSettings.RetryDelay);
            }
        }
    }
}
