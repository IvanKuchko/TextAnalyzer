namespace TextAnalyzer.Client.Models.Requests
{
    public class SignInRequest : BaseRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
