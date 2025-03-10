using System;
using System.Net;
using System.Net.Mail;
using Domain;
using Domain.Interfaces.Services;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string toName, string toEmail, string subject, string body, string fromName,
        string fromEmail, CancellationToken cancellationToken)
    {
        var smtp = new SmtpClient(Configuration.SmtpServer, Configuration.SmtpPort)
        {
            Credentials = new NetworkCredential(Configuration.SmtpUser, Configuration.SmtpPass),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = true
        };

        var mail = new MailMessage
        {
            From = new MailAddress(fromEmail, fromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        mail.To.Add(new MailAddress(toEmail, toName));
        await smtp.SendMailAsync(mail, cancellationToken);
    }
}