namespace UserAPI.Models.ResponseModels
{
    public class UserResponseSignInModel
    {
        public string JwtToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
