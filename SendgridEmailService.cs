using SendGrid;
using SendGrid.Helpers.Mail;

namespace ConsoleWindowsHosting;

public class SendgridEmailService : IEmailService
{
    private readonly EmailConfig _emailConfig;

    public SendgridEmailService()
    {
        _emailConfig = new EmailConfig();
    }

    public async Task SendEmailAsync(string subject, string body,
                                     List<EmailAddress> receivers, bool isHtmlContent = false,
                                     CancellationToken cancellationToken = new CancellationToken())
    {
        var client = new SendGridClient(_emailConfig.SendgridApiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(_emailConfig.DefaultSender),
            Subject = subject,
            HtmlContent = isHtmlContent ? body : string.Empty,
            PlainTextContent = isHtmlContent ? string.Empty : body
        };
        msg.AddTos(receivers);
        var response = await client.SendEmailAsync(msg, cancellationToken);
    }
}