namespace Common.Options;

public class SendGridOptions
{
    public string ApiKey { get; set; }
    public string FromEmail { get; set; }
    public string NameOfSender { get; set; }
}