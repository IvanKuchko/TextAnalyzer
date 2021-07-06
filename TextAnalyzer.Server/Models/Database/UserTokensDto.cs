namespace TextAnalyzer.Server.Models.Database
{
    public class UserTokensDto : BaseDto
    {
        public UserDto User { get; set; }
        public string AccessToken { get; set; }
        public int ExpireIn { get; set; }
        public string RefreshToken { get; set; }
    }
}
