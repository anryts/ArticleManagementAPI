using APIGateway.Providers.Interfaces;
using Common.Extensions;
using Common.Options;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace APIGateway.Providers;

public class SengGridProvider : IEmailProvider
{
    private readonly ISendGridClient _sendGridClient;
    private readonly IOptions<SendGridOptions> _options;

    public SengGridProvider(ISendGridClient sendGridClient,
        IOptions<SendGridOptions> options)
    {
        _sendGridClient = sendGridClient;
        _options = options;
    }

    /// <summary>
    /// Send email to user with subject and content
    /// </summary>
    /// <param name="to">User email</param>
    /// <param name="subject">Name of email</param>
    /// <param name="nameOfReceiver">first and last name for receiver</param>
    /// <param name="content">Content in html format</param>
    /// <returns></returns>
    public async Task SendEmailAsync(string to, string subject, string nameOfReceiver, string content)
    {
        var fromEmailAddress = new EmailAddress(_options.Value.FromEmail, _options.Value.NameOfSender);
        var toEmailAddress = new EmailAddress(to, nameOfReceiver);
        var msg = MailHelper.CreateSingleEmail(fromEmailAddress, toEmailAddress, subject,
            content.GetShortIntroFromContent(), content);
        await _sendGridClient.SendEmailAsync(msg);
    }
}