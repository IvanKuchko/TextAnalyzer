namespace TextAnalyzer.Server.Models
{
    public class Tokens
    {
        public string AccessToken { get; set; }
        public int ExpireIn { get; set; }
        public string RefreshToken { get; set; }
    }
}
