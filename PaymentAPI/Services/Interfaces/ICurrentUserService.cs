namespace UserAPI.API.Services.Interfaces
{
    public interface ICurrentUserService
    {
        Guid GetCurrentUserId();
    }
}