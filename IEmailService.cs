using SendGrid.Helpers.Mail;

namespace ConsoleWindowsHosting;

public interface IEmailService
{
    Task SendEmailAsync(string subject, string body,
                                     List<EmailAddress> receivers, bool isHtmlContent = false,
                                     CancellationToken cancellationToken = new CancellationToken());
}