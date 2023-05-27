namespace UserAPI.Providers.Interfaces;

public interface ISmsProvider
{
    Task SendSmsAsync(string phoneNumber, string message);
}