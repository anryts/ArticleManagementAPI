namespace ArticleAPI.Providers.Interfaces;

public interface IEmailProvider
{
    /// <summary>
    /// Send email to user with subject and content
    /// </summary>
    /// <param name="to">User email</param>
    /// <param name="subject">Name of email</param>
    /// <param name="nameOfReceiver">name for receiver</param>
    /// <param name="content">Content in html format</param>
    /// <returns></returns>
    Task SendEmailAsync(string to, string subject, string nameOfReceiver, string content);
}