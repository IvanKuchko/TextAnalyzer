namespace TextAnalyzer.Server.Models.Database
{
    public class UserDto : BaseDto
    {       
        public string Login { get; set; }
        public string NormalizedLogin { get; set; }
        public string Email { get; set; }  
        public string NormalizedEmail { get; set; }  
        public string PasswordHash { get; set; }              
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
