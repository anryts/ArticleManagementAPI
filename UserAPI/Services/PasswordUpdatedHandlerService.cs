using UserAPI.API.Services.Interfaces;

namespace UserAPI.Services;

public class  PasswordUpdatedHandlerService : IPasswordUpdatedHandlerService
{
    private readonly Dictionary<Guid, DateTime> _changedPasswords;

    public PasswordUpdatedHandlerService()
    {
        _changedPasswords = new Dictionary<Guid, DateTime>();
    }

    public void AddToDictionary(Guid userId, DateTime passwordUpdatedAt)
    {
        if(!_changedPasswords.ContainsKey(userId))
            _changedPasswords.Add(userId, passwordUpdatedAt);

        _changedPasswords[userId] = passwordUpdatedAt;  
    }

    public DateTime GetDateTime(Guid userId) 
    {
        if(_changedPasswords.TryGetValue(userId, out var passwordUpdatedAt))
            return passwordUpdatedAt;

        return DateTime.MinValue;
    }
}