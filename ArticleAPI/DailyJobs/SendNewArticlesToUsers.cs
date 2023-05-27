using System.Text;
using ArticleAPI.Providers.Interfaces;
using ArticleAPI.Services.Interfaces;
using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArticleAPI.DailyJobs;

public class SendNewArticlesToUsers
{
    private const string EmailSubject = "Your Daily Articles";
    private readonly IArticleRepository _articleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailProvider _emailProvider;
    private readonly IBackgroundJobService _backgroundJobService;

    public SendNewArticlesToUsers(IArticleRepository articleRepository,
        IUserRepository userRepository,
        IEmailProvider emailProvider, IBackgroundJobService backgroundJobService)
    {
        _articleRepository = articleRepository;
        _userRepository = userRepository;
        _emailProvider = emailProvider;
        _backgroundJobService = backgroundJobService;
    }

    public void Execute(string cronExpression)
    {
        _backgroundJobService.CronJob(() => SendEmails(EmailSubject), cronExpression);
    }

    public async Task SendEmails(string subject)
    {
        var dictionary = await GetArticlesAsync();
        foreach (var user in dictionary)
        {
            var articles = user.Value;
            var body = new StringBuilder();
            foreach (var article in articles)
            {
                body.Append(GenerateNewArticleNotificationHtml(article.Title, article.ShortIntro));
            }

            await _emailProvider.SendEmailAsync(user.Key.Email,
                EmailSubject,
                user.Key.FirstName + user.Key.LastName,
                body.ToString());
        }
    }

    private async Task<Dictionary<User, IEnumerable<Article>>> GetArticlesAsync()
    {
        var yesterday = DateTime.UtcNow.AddDays(-1);
        var users = await _userRepository.GetAllAsync();
        var articles = await _articleRepository
            .GetByQueryAsync(q => q
                .Include(x => x.Channel)
                .Where(article => article.CreatedAt > yesterday));
        Dictionary<User, IEnumerable<Article>> dictionary = new();
        foreach (var user in users)
        {
            if (user.IsSubscriptionActive)
                dictionary.Add(user, articles);
            dictionary.Add(user, articles.Where(article => article.Channel is null));
        }

        return dictionary;
    }

    private string GenerateNewArticleNotificationHtml(string articleTitle, string articleIntro)
    {
        return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""utf-8"">
                </head>
                <body style=""font-family: Arial, sans-serif; background-color: #F4F4F4; color: #333333; padding: 20px;"">
                    <div style=""background-color: #FFFFFF; padding: 20px; border: 1px solid #CCCCCC;"">
                        <h2 style=""font-size: 20px; margin-bottom: 10px;"">{articleTitle}</h2>
                        <p style=""font-size: 16px;"">{articleIntro}</p>
                    </div>
                </body>
                </html>
            ";
    }
}