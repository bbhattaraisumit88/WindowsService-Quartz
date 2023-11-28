namespace ConsoleWindowsHosting;

public class EmailConfig
{
    public string SendgridApiKey { get; set; }
    public string SupportEmail { get; set; }
    public string DefaultSender { get; set; }

    public EmailConfig()
    {
        SendgridApiKey = "";
        DefaultSender = "";
        SupportEmail = "";
    }
}