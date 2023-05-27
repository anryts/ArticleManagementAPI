namespace Common.Models.ResponseModels
{
    public class RefreshTokenResponseModel
    {
        public string AuthToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
