namespace UserAPI.API.Services.Interfaces
{
    public interface IPasswordUpdatedHandlerService
    {
        public void AddToDictionary(Guid userId, DateTime passwordUpdatedAt);
        public DateTime GetDateTime(Guid userId);
    }
}