using Quartz;

using SendGrid.Helpers.Mail;

namespace ConsoleWindowsHosting;

[DisallowConcurrentExecution]
public class LongRunningJob : IJob
{
    private readonly IEmailService _emailService;

    public LongRunningJob(IEmailService emailService) => _emailService = emailService;

    public async Task Execute(IJobExecutionContext context)
    {
        var receivers = new List<EmailAddress>()
        {
            new EmailAddress("sbhattarai@qkly.io")
        };
        string message = "Task Completed";
        await LongRunningTaskAsync();
        _ = SendEmailAsync(message, receivers);
        Console.WriteLine(message);
    }

    private async Task SendEmailAsync(string message, List<EmailAddress> receivers)
    {
        await _emailService.SendEmailAsync(message, "Hi, Console App Task Completed.", receivers);
    }

    private async Task LongRunningTaskAsync()
    {
        await Task.Delay(10000);
    }
}