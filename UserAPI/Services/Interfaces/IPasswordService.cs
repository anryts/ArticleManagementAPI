namespace UserAPI.API.Services.Interfaces
{
    public interface IPasswordService
    {
        public string GenerateHashPassword(string password);
        public bool VerifyHashPassword(string password, string hash);
    }
}