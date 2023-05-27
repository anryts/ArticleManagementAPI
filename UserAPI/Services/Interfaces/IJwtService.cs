namespace UserAPI.API.Services.Interfaces
{
    public interface IJwtService
    {
        public string CreateJwt(Guid id);
    }
}
