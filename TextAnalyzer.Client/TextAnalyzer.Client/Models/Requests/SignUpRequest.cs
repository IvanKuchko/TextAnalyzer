namespace TextAnalyzer.Client.Models.Requests
{
    public class SignUpRequest : BaseRequest
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
